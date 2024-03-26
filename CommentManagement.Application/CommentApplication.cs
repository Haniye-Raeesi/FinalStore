using _0_FrameWork.Application;
using CommentManagement.Application.Contract;
using CommentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operationResult = new OperationResult();
            var comment = new Comment(command.Name,command.Email,command.Message,command.Website,command.OwnerRecordId
                ,command.Type,command.ParentId);
            _commentRepository.Create(comment);
            _commentRepository.Save();
            return operationResult.Successful();
        }

        public OperationResult Cancel(long Id)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(Id);
            if (comment==null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            comment.Cancel();
            _commentRepository.Save();
            return operationResult.Successful();
        }

        public OperationResult Confirm(long Id)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(Id);
            if (comment == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            comment.Confirm();
            _commentRepository.Save();
            return operationResult.Successful();
        }

        public List<CommentViewModel> Search(CommentSearchModel command)
        {
          return  _commentRepository.Search(command);
        }
    }
}
