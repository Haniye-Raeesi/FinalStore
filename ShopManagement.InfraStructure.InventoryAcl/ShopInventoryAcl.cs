using InventoryManagement.Application.Contracts;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.InfraStructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceInventory(List<OrderItem> items)
        {
            var command = items.Select(orderItem =>
                    new DecreaseInventory(orderItem.ProductId, orderItem.Count, "خرید مشتری", orderItem.OrderId))
                .ToList();

            return _inventoryApplication.Decrease(command).IsSuccedded;
        }
    }
}