using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<SlideViewModel> Slides;
        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long Id)
        {
            var slide1 = _slideApplication.GetDetails(Id);
            return Partial("./Edit", slide1);
        }
        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var ProductPicture2 = _slideApplication.Remove(id);
            if (ProductPicture2.IsSuccedded)
            {
               return RedirectToPage("./Index");
            }
            Message = ProductPicture2.Message;
           return RedirectToPage("./Index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var ProductPicture2 = _slideApplication.Restore(id);
            if (ProductPicture2.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            Message = ProductPicture2.Message;
            return RedirectToPage("./Index");

        }

    }


}
