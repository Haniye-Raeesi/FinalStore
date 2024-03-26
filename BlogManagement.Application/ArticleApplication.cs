using _0_Framework.Application;
using _0_FrameWork.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, 
            IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operationResult = new OperationResult();
            if (_articleRepository.Exists(x=>x.Title==command.Title))
            {
                return operationResult.Failed(ApplicationMessages.Duplicated);
            }
            else
            {
                var Slug = command.Slug.Slugify();
                var publishDate = command.PublishDate.ToGeorgianDateTime();
                var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
                var PicturePath= $"{categorySlug}/{Slug}";
                var fileName = _fileUploader.Upload(command.Picture, PicturePath);
                var article = new Article(command.Title, command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, publishDate,
                Slug, command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);
                _articleRepository.Create(article);
                _articleRepository.Save();
                return operationResult.Successful();
            }
            
        }

        public OperationResult Edit(EditArticle command)
        {
            //var operationResult = new OperationResult();
            //var article = _articleRepository.GetArticleWithCategory(command.Id);
            //if (_articleRepository.Exists(x => x.Title == command.Title))
            //{
            //    return operationResult.Failed(ApplicationMessages.Duplicated);
            //}
            //if (article==null)
            //{
            //    return operationResult.Failed(ApplicationMessages.RecordNotFound);
            //}
            //if (_articleRepository.Exists(x=>x.Title==command.Title && x.Id==command.Id))
            //{
            //    return operationResult.Failed(ApplicationMessages.Duplicated);
            //}
            // var Slug = command.Slug.Slugify();
            // var publishDate = command.PublishDate.ToGeorgianDateTime();
            // var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            // var PicturePath = $"{categorySlug}/{Slug}";
            // var fileName = _fileUploader.Upload(command.Picture, PicturePath);

            // article.Edit(command.Title, command.ShortDescription, command.Description,
            // fileName, command.PictureAlt, command.PictureTitle, publishDate,
            // Slug, command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);
            // _articleRepository.Save();
            //  return operationResult.Successful();

            var operation = new OperationResult();
            var article = _articleRepository.GetArticleWithCategory(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.Duplicated);

            var slug = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.ShortDescription, command.Description, pictureName,
                command.PictureAlt, command.PictureTitle, publishDate, slug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress, command.CategoryId);

            _articleRepository.Save();
            return operation.Successful();

        }

        public EditArticle GetDetails(long Id)
        {
            return _articleRepository.GetDetails(Id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel SearchModel)
        {
            return _articleRepository.Search(SearchModel);
        }
    }
}
