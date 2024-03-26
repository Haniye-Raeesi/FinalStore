using _0_FrameWork.Domain;
using BlogManagement.Application.Contracts.Article;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        List<ArticleViewModel> Search(ArticleSearchModel SearchModel);
        EditArticle GetDetails(long Id);
        Article GetArticleWithCategory(long Id);
    }
}
