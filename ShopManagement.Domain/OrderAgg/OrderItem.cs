using _0_FrameWork.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class OrderItem:EntityBase
    {
        public long ProductId { get; private set; }
        public int Count { get; private set; }
        public double UnitPrice { get; private set; }
        public int DiscountRate { get; private set; }
        public long OrderId { get;  set; }
        public Order Order { get; private set; }
        protected OrderItem()
        {

        }

        public OrderItem(long productId, int count, double unitPrice, int discountRate)
        {
            ProductId = productId;
            Count = count;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
        }
    }
}
