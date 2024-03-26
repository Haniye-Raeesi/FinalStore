using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discounts
{
    public class IndexModel : PageModel
    {
        public List<CustomerDiscountViewModel> Discounts;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public CustomerDiscountSearchModel SearchModel;
        public SelectList Products;
        [TempData]
        public string Message { get; set; }

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");

            Discounts = _customerDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long Id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(Id);
            customerDiscount.Products = _productApplication.GetProducts();
            return Partial("./Edit",customerDiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result=_customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
        
    }

    
}
