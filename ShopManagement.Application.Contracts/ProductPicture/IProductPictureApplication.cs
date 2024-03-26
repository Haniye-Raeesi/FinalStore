using _0_FrameWork.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        EditProductPicture GetDetails(long id);
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        OperationResult Remove(long Id);
        OperationResult Restore(long Id);
    }
}
