using _0_FrameWork.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel SearchModel);
        public OperationResult Create(CreateArticleCategory command);
        public OperationResult Edit(EditArticleCategory command);
        public List<ArticleCategoryViewModel> GetArticleCategories();
        EditArticleCategory GetDetails(long Id);
    }
}
