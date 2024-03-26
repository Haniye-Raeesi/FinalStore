using _0_Framework.Application;
using _0_FrameWork.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository productRepository,IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var Operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
            { return Operation.Failed(ApplicationMessages.Duplicated); }
            else
            {
                var slug = command.Slug.Slugify();
                var PicturePath = $"{slug}";
                var fileName = _fileUploader.Upload(command.Picture, PicturePath);

                var product = new Product(command.Name, command.Code, command.Description,
                    fileName, command.PictureAlt, command.PictureTitle, command.KeyWords
                    , command.MetaDescrioption, slug, command.CategoryId);
                _productRepository.Create(product);
                _productRepository.Save();
                return Operation.Successful();

            }

        }

        public OperationResult Edit(EditProduct command)
        {
            var product = _productRepository.GetProductWithCategory(command.Id);
            var Operation = new OperationResult();
            if (_productRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return Operation.Failed(ApplicationMessages.Duplicated);
            }
            if (product==null)
            {
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var slug = command.Slug.Slugify();
            var PicturePath = $"{slug}";
            var fileName = _fileUploader.Upload(command.Picture, PicturePath);

            product.Edit(command.Name, command.Code, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.KeyWords,
                command.MetaDescrioption, slug, command.CategoryId);
            _productRepository.Save();
            return Operation.Successful();
  
        }

        public EditProduct GetDetails(long Id)
        {
            return _productRepository.GetDetails(Id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
           return _productRepository.Search(searchModel);
        }
        

    }
}
