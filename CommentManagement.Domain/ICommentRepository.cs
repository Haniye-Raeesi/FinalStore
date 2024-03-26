using _0_FrameWork.Application;
using _0_FrameWork.Domain;
using CommentManagement.Application.Contract;
using System.Collections.Generic;

namespace CommentManagement.Domain
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel command);
    }
}
