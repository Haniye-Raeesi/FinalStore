using _0_FrameWork.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IRepository<long,CustomerDiscount>
    {
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
        EditCustomerDiscount GetDetails(long Id);
    }
}
