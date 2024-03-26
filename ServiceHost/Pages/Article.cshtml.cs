using _01_StoreQuery.Contracts.Article;
using _01_StoreQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contract;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleQueryModel Article;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;

        public ArticleModel(IArticleQuery articleQuery, ICommentApplication commentApplication,IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string Id)
        {
            Article = _articleQuery.GetDetails(Id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
        }
        public IActionResult OnPost(AddComment command, string ArticleSlug) 
        {
            command.Type = CommentType.Article;
            _commentApplication.Add(command);
            return RedirectToPage("/Article", new { id = ArticleSlug });
        }
    }
}
