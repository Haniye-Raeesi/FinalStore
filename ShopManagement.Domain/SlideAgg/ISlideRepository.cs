using _0_FrameWork.Domain;
using ShopManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository:IRepository<long,Slide>
    {
        EditSlide GetDetails(long Id);
        public List<SlideViewModel> GetList();
    }
}
