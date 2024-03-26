namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {

        public long Id { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long OperatorId { get; private set; }
        public string Description { get; private set; }
        public long Count { get; private set; }
        public bool Operation { get; private set; }
        public long OrderId { get; private set; }
        public long CurrentCount { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation()
        {
        }


        public InventoryOperation(long operatorId, string description, long count,
            bool operation, long orderId, long currentCount, long inventoryId)
        {
            OperatorId = operatorId;
            Description = description;
            Count = count;
            Operation = operation;
            OrderId = orderId;
            CurrentCount = currentCount;
            InventoryId = inventoryId;
            OperationDate=DateTime.Now;
        }

    }

}