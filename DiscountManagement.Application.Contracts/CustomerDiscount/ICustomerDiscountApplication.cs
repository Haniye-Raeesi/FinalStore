using _0_FrameWork.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
        EditCustomerDiscount GetDetails(long Id);
    }
}
