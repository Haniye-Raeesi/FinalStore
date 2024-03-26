using _0_FrameWork.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);
        OperationResult Remove(long Id);
        OperationResult Restore(long Id);
        public List<SlideViewModel> GetList();
        EditSlide GetDetails(long Id);

    }
    
}
