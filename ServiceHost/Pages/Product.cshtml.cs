using _01_StoreQuery.Contracts.Product;
using CommentManagement.Application.Contract;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }
        public IActionResult OnPost(AddComment command,string ProductSlug) 
        {
            command.Type = CommentType.Product;
            _commentApplication.Add(command);
           return RedirectToPage("/Product", new {id=ProductSlug });
        }

    }
}
