using _0_FrameWork.InfraStructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Application;
using ShopManagement.InfraStructure.EfCore;

namespace DiscountManagement.InfraStructure.EfCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _DiscountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext):base(discountContext)
        {
            _DiscountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long Id)
        {
            return _DiscountContext.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString()

            }).FirstOrDefault(x => x.Id == Id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var Products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _DiscountContext.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                StartDateGr = x.StartDate,
                EndDateGr = x.EndDate,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi()
            }) ;

            if (searchModel.ProductId>0)
            {
                query = query.Where(x=>x.ProductId==searchModel.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x=>x.StartDateGr>searchModel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }
            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discount =>
                discount.Product = Products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return discounts;


        }
    }
}
