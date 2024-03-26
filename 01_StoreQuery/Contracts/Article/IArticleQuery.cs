using System.Collections.Generic;

namespace _01_StoreQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetArticles();
        ArticleQueryModel GetDetails(string slug);
        List<ArticleQueryModel> LatestArticles();

    }
}
