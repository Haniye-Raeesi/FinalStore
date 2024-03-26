using _0_FrameWork.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long,ArticleCategory>
    {
       public string GetSlugBy(long Id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel SearchModel);
        EditArticleCategory GetDetails(long Id);
        List<ArticleCategoryViewModel> GetArticleCategories();


    }
}
