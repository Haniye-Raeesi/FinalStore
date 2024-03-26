using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository: RepositoryBase<long, Article>,IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context):base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long Id)
        {
            return _context.Articles.Select(x => new EditArticle
            {Id=x.Id,
            Title=x.Title,
            //Picture=x.Picture,
            PictureAlt=x.PictureAlt,
            PictureTitle=x.PictureTitle,
            Slug=x.Slug,
            Keywords=x.Keywords,
            MetaDescription=x.MetaDescription,
            CanonicalAddress=x.CanonicalAddress,
            PublishDate=x.PublishDate.ToFarsi(),
            CategoryId=x.CategoryId,
            ShortDescription=x.ShortDescription,
            Description=x.Description

            }).FirstOrDefault(x=>x.Id==Id);
        }

        public Article GetArticleWithCategory(long Id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == Id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel SearchModel)
        {
            var query = _context.Articles
                .Include(x => x.Category)
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    Title=x.Title,
                    Picture = x.Picture,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    ShortDescription=x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi()
                }).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(SearchModel.Title))
                query = query.Where(x => x.Title.Contains(SearchModel.Title));
            if (SearchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == SearchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
    }

