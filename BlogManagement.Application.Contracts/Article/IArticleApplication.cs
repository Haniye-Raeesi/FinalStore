using _0_FrameWork.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        public List<ArticleViewModel> Search(ArticleSearchModel SearchModel);
        public OperationResult Create(CreateArticle command);
        public OperationResult Edit(EditArticle command);
         EditArticle GetDetails(long Id);

    }
}
