using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository: RepositoryBase<long, ArticleCategory>,IArticleCategoryRepository
    {
        private readonly BlogContext _Context;

        public ArticleCategoryRepository(BlogContext context):base(context)
        {
            _Context = context;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _Context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditArticleCategory GetDetails(long Id)
        {
            return _Context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                CanonicalAddress = x.CanonicalAddress,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x=>x.Id==Id);
        }

        public string GetSlugBy(long Id)
        {
            return _Context.ArticleCategories.FirstOrDefault(x=>x.Id==Id).Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel SearchModel)
        {
            var query = _Context.ArticleCategories
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    CreationDate = DateTime.Now.ToFarsi()

                }).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(SearchModel.Name))
                query = query.Where(x => x.Name.Contains(SearchModel.Name));
            

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
