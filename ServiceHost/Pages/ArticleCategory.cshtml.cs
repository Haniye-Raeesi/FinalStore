using _01_StoreQuery.Contracts.Article;
using _01_StoreQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
       public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery,IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string Id)
        {
            ArticleCategory= _articleCategoryQuery.GetArticleCategoryWithArticles(Id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
