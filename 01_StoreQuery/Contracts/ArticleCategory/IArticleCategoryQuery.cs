using System.Collections.Generic;

namespace _01_StoreQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        public List<ArticleCategoryQueryModel> GetArticleCategories();
        public ArticleCategoryQueryModel GetArticleCategoryWithArticles(string Slug);
    }
}
