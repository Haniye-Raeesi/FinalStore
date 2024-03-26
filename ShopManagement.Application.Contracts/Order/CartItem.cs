using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public double unitPrice { get; set; }
        public double DoublePrice { get; set; }
        public int count { get; set; }
        public double TotalItemPrice { get; set; }
        public string Picture { get; set; }
        public bool IsInStock { get; set; }
        public double DiscountedTotalItemPrice { get; set; }
        public double TotPriceAfterDisc { get; set; }

        public int Discount { get; set; }


        public CartItem()
        {
            TotalItemPrice = count * DoublePrice;
        }


    }
}
