using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.Services
{
    public interface IShopInventoryAcl
    {
        public bool ReduceInventory(List<OrderItem> items);
    }
}
