using _0_Framework.Application;
using _0_FrameWork.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{

    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

    public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository,IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

    public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
            {
               return operation.Failed(ApplicationMessages.Duplicated);
            }
            else
            {
                var Slug = command.Slug.Slugify();
                var PicturePath = $"{Slug}";
                var fileName = _fileUploader.Upload(command.Picture, PicturePath);

                var productCategory = new ProductCategory(command.Name,command.Description,fileName
                    ,command.PictureAlt,command.PictureTitle,command.KeyWords,command.MetaDescrioption,Slug);
                _productCategoryRepository.Create(productCategory);
                _productCategoryRepository.Save();
                return operation.Successful();
            }
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.Duplicated);

            }
            var Slug = command.Slug.Slugify();
            var PicturePath = $"{Slug}";
           var fileName=_fileUploader.Upload(command.Picture,PicturePath);
            

            productCategory.Edit(command.Name,command.Description,fileName
                ,command.PictureAlt,command.PictureTitle,command.KeyWords,command.MetaDescrioption,Slug);
            _productCategoryRepository.Save();
            return operation.Successful();
        }

        public EditProductCategory GetDetails(long Id)
        {
            return _productCategoryRepository.GetDetails(Id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

       
    }
}
