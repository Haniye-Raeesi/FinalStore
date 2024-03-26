using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;
        public ProductPictureSearchModel SearchModel;
        public SelectList Productss;
        public List<ProductPictureViewModel> ProductPictures;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Productss = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long Id)
        {

            var ProductPicture1 = _productPictureApplication.GetDetails(Id);
            ProductPicture1.Products = _productApplication.GetProducts();
            return Partial("./Edit", ProductPicture1);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var ProductPicture2 = _productPictureApplication.Remove(id);
            if (ProductPicture2.IsSuccedded)
            {
               return RedirectToPage("./Index");

            }

            Message = ProductPicture2.Message;
           return RedirectToPage("./Index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var ProductPicture2 = _productPictureApplication.Restore(id);
            if (ProductPicture2.IsSuccedded)
            {
                return RedirectToPage("./Index");

            }

            Message = ProductPicture2.Message;
            return RedirectToPage("./Index");

        }

    }


}
