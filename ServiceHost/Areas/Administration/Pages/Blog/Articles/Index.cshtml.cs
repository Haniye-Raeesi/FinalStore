using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        public List<ArticleViewModel> Articles;
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public ArticleSearchModel SearchModel;
        public SelectList ArticleCategories;
        [TempData]
        public string Message { get; set; }

        public IndexModel(IArticleApplication productApplication, IArticleCategoryApplication productCategoryApplication)
        {
            _articleApplication = productApplication;
            _articleCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");

            Articles = _articleApplication.Search(searchModel);
        }

    }
}
