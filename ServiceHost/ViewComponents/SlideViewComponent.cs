using _01_StoreQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;
namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent: ViewComponent
    {
        private readonly ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }
        public IViewComponentResult Invoke()
        {
            var Slides = _slideQuery.GetSlides();
            return View(Slides);
        }
    }
}
