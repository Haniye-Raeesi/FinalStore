using _0_FrameWork.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long Id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
        
    }
}
