using _0_Framework.Application;
using _0_FrameWork.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository
            , IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operationResult = new OperationResult();
            if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
            {
                return operationResult.Failed(ApplicationMessages.Duplicated);
            }
            else
            {
                var Slug = command.Slug.Slugify();
                var PicturePath = $"{Slug}";
                var fileName = _fileUploader.Upload(command.Picture, PicturePath);
                var articleCategory = new ArticleCategory(command.Name, fileName, command.PictureAlt, command.PictureTitle
                    , command.Description, command.ShowOrder, command.Slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
                _articleCategoryRepository.Create(articleCategory);
                _articleCategoryRepository.Save();
                return operationResult.Successful();
            }

        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operationResult = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);
            if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
            {
                return operationResult.Failed(ApplicationMessages.Duplicated);
            }
            if (articleCategory == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id == command.Id))
            {
                return operationResult.Failed(ApplicationMessages.Duplicated);
            }
            var Slug = command.Slug.Slugify();
            var PicturePath = $"{Slug}";
            var fileName = _fileUploader.Upload(command.Picture, PicturePath);

            articleCategory.Edit(command.Name, fileName, command.PictureAlt, command.PictureTitle
                , command.Description, command.ShowOrder, command.Slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.Save();
            return operationResult.Successful();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }

        public EditArticleCategory GetDetails(long Id)
        {
            return _articleCategoryRepository.GetDetails(Id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel SearchModel)
        {
            return _articleCategoryRepository.Search(SearchModel);
        }
    }

       

        
    }

