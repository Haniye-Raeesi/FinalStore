using _0_FrameWork.Domain;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<long,ProductPicture>
    {
        List<ProductPictureViewModel>Search(ProductPictureSearchModel searchModel);
        EditProductPicture GetDetails(long Id);
        ProductPicture GetByProductAndCategory(long id);

    }
}
