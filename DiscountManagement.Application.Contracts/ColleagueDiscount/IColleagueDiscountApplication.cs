using _0_Framework.Application;
using _0_FrameWork.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
