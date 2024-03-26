using ShopManagement.Application.Contracts.Order;
using System.Collections.Generic;

namespace _01_StoreQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string Value);
        ProductQueryModel GetDetails(string slug);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
        

    }
}
