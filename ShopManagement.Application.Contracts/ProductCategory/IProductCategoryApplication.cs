using System.Collections.Generic;
using _0_FrameWork.Application;
namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        EditProductCategory GetDetails(long Id);
        List<ProductCategoryViewModel> GetProductCategories();


    }
}
