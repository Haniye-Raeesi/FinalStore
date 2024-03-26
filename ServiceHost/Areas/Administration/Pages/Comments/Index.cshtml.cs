using CommentManagement.Application.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private readonly ICommentApplication _commentApplication;
        public CommentSearchModel SearchModel;
        public List<CommentViewModel> Comments;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }
        public IActionResult OnGetCancel(long id) 
        {
            var comment = _commentApplication.Cancel(id);
            if (comment.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }

            Message = comment.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetConfirm(long id)
        {
            var comment = _commentApplication.Confirm(id);
            if (comment.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }

            Message = comment.Message;
            return RedirectToPage("./Index");
        }
    }
}
