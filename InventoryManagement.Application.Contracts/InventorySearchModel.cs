namespace InventoryManagement.Application.Contracts
{
    public class InventorySearchModel
    {
        public long ProductId { get; private set; }
        public bool IsInStock { get; private set; }

    }
}
