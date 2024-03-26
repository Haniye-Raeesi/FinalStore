using _0_FrameWork.Domain;
using System;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {

        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public string Reason { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

    

        public CustomerDiscount(long productId, int discountRate,
            string reason, DateTime startDate, DateTime endDate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
        }
        public void Edit(long productId, int discountRate,
            string reason, DateTime startDate,DateTime endDate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
        }


    }


}
