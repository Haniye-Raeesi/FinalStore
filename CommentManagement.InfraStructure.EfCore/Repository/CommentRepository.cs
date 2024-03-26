using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using CommentManagement.Application.Contract;
using CommentManagement.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository:RepositoryBase<long,Comment>, ICommentRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext):base(commentContext)
        {
            _commentContext = commentContext;
        }


        public List<CommentViewModel> Search(CommentSearchModel command)
        {
            var query = _commentContext.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                CommentDate = x.CommentDate.ToFarsi()

            }) ;
            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                query = query.Where(x => x.Name == command.Name);
            }
            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                query = query.Where(x=>x.Email==command.Email);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
