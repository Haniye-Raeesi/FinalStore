using _0_FrameWork.Application;
using InventoryManagement.Application.Contracts;
using InventoryManagement.Domain.InventoryAgg;
using System.Collections.Generic;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var Operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            { return Operation.Failed(ApplicationMessages.Duplicated); }
            else
            {

                var inv = new Inventory(command.ProductId, command.UnitPrice);
                _inventoryRepository.Create(inv);
                _inventoryRepository.Save();
                return Operation.Successful();

            }
        }
        public OperationResult Edit(EditInventory command)
        {
            var operationResult = new OperationResult();
            var inv = _inventoryRepository.Get(command.Id);
            if (inv == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicated);


            inv.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.Save();
            return operationResult.Successful();
        }


        public EditInventory GetDetails(long Id)
        {
            return _inventoryRepository.GetDetails(Id);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var Operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var OperatorId = 1;
            inventory.Increase(command.Count, OperatorId, command.Description);
            _inventoryRepository.Save();
            return Operation.Successful();

        }
        public OperationResult Decrease(DecreaseInventory command)
        {
            var Operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var OperatorId = 1;
            inventory.Decrease(command.count, OperatorId, command.Discription, 0);
            _inventoryRepository.Save();
            return Operation.Successful();
        }

        public OperationResult Decrease(List<DecreaseInventory> command)
        {
            var Operation = new OperationResult();
            if (command == null)
            {
                Operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var OperatorId = 1;
            foreach (var item in command)
            {
                var inv = _inventoryRepository.Get(item.InventoryId);
                inv.Decrease(item.count, OperatorId, item.Discription, item.OrderId);
            }
            _inventoryRepository.Save();
            return Operation.Successful();
        }


        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long InventoryId)
        {
            return _inventoryRepository.GetOperationLog(InventoryId);
        }
    }

}
