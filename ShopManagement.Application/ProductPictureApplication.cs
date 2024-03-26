using _0_FrameWork.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository
            ,IFileUploader fileUploader
            ,IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var Operation = new OperationResult();
            //if (_productPictureRepository.Exists(x=>x.ProductId==command.ProductId && x.Picture==command.Picture))
            //{
            //    return Operation.Failed(ApplicationMessages.Duplicated);
            //}
            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var PicturePath = $"{product.Category.Slug}//{product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, PicturePath);

            var ProductPicture = new ProductPicture(command.ProductId,fileName,command.PictureAlt,command.PictureTitle);
            _productPictureRepository.Create(ProductPicture);
            _productPictureRepository.Save();
            return Operation.Successful();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var ProductPicture = _productPictureRepository.GetByProductAndCategory(command.Id);
            if (ProductPicture==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var PicturePath = $"{product.Category.Slug}//{product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, PicturePath);



            ProductPicture.Edit(command.ProductId,fileName,command.PictureAlt,command.PictureTitle);
            _productPictureRepository.Save();
            return operation.Successful();
            
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long Id)
        {
            var operation = new OperationResult();
            var ProductPicture = _productPictureRepository.Get(Id);
            if (ProductPicture==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            ProductPicture.Remove();
            _productPictureRepository.Save();
            return operation.Successful();
        }

        public OperationResult Restore(long Id)
        {
            var operation = new OperationResult();
            var ProductPicture = _productPictureRepository.Get(Id);
            if (ProductPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            ProductPicture.Restore();
            _productPictureRepository.Save();
            return operation.Successful();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
