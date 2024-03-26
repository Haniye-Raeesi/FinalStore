using _0_FrameWork.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(DecreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        EditInventory GetDetails(long Id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long InventoryId);

    }
}
