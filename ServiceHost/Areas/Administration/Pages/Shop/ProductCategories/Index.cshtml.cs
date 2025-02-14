using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    // [Authorize(Roles ="1") ]
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories=_productCategoryApplication.Search(searchModel);

        }
        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CreateProductCategory());
        }
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
           var result= _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long Id)
        {
            var pCategory = _productCategoryApplication.GetDetails(Id);
            return Partial("./Edit",pCategory);

        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}

