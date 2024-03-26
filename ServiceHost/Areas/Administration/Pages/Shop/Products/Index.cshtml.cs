using _0_FrameWork.InfraStructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.InfraStructure.Configuration.Permission;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductViewModel> Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public ProductSearchModel SearchModel;
        public SelectList ProductCategories;
        [TempData]
        public string Message { get; set; }

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(),"Id","Name");

            Products = _productApplication.Search(searchModel);
        }
        [NeedsPermissions(ShopPermissions.CreateProduct)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long Id)
        {
            var product = _productApplication.GetDetails(Id);
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit",product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result=_productApplication.Edit(command);
            return new JsonResult(result);
        }
       

    }

    
}
