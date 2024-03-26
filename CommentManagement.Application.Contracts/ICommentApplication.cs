using _0_FrameWork.Application;
using System.Collections.Generic;

namespace CommentManagement.Application.Contract
{
    public interface ICommentApplication
    {
        public OperationResult Confirm(long Id);
        public OperationResult Cancel(long Id);
        List<CommentViewModel> Search(CommentSearchModel command);
        public OperationResult Add(AddComment command);
    }
}
