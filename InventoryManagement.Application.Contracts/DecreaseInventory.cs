namespace InventoryManagement.Application.Contracts
{
    public class DecreaseInventory
    {
       
        public long InventoryId { get; set; }
        public long count { get; set; }
        public string Discription { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }


        public DecreaseInventory(long inventoryId, long count, string discription, long orderId)
        {
            InventoryId = inventoryId;
            this.count = count;
            Discription = discription;
            OrderId = orderId;
        }

        public DecreaseInventory()
        {
        }
    }
}
