using _0_FrameWork.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{

    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public List<InventoryOperation> Operations { get; set; }

        public Inventory()
        {
            Operations = new List<InventoryOperation>();   
        }
        

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }


        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }
        public long CalculateCurrentCount()
        {

            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count, long OperatorId, string Disc)
        {
            var currentCount = count + CalculateCurrentCount();
            var Operation = new InventoryOperation(OperatorId, Disc, count, true, 0, currentCount, Id);
            Operations.Add(Operation);
            IsInStock = currentCount > 0;
        }
        public void Decrease(long count, long OperatorId, string Disc, long OrderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var Operation = new InventoryOperation(OperatorId, Disc, count, false, OrderId, currentCount, Id);
            Operations.Add(Operation);
            IsInStock = currentCount > 0;
        }


    }


}
