using _0_FrameWork.Domain;
using InventoryManagement.Application.Contracts;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        EditInventory GetDetails(long Id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        Inventory GetBy(long productId);
        List<InventoryOperationViewModel> GetOperationLog(long InventorryId);

    }
}
