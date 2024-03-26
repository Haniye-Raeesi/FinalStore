using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using AccountManagement.InfraStructure.EfCore;
using InventoryManagement.Application.Contracts;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.InfraStructure.EfCore;

namespace InventoryManagement.Infrastructure.EfCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly AccountContext _accountContext;
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public Inventory GetBy(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long InventoryId)
        {
            var accounts= _accountContext.Accounts;
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x=>x.Id==InventoryId);
            var operations= inventory.Operations.Select(x => new InventoryOperationViewModel
            {   Id = x.Id,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
                OrderId=x.OrderId,
                Count=x.Count,
                CurrentCount=x.CurrentCount,
                OperatorId=x.OperatorId,
                Description=x.Description


            }).ToList();
            foreach (var operation in operations)
            {
                operation.Operator = _accountContext.Accounts.Select(x=> new {x.Id,x.FullName }).
                    FirstOrDefault(x=>x.Id==operation.OperatorId)?.FullName;
            }
            return operations;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                IsInStock = x.IsInStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.IsInStock)
                query = query.Where(x => !x.IsInStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item =>
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }
    }



}
