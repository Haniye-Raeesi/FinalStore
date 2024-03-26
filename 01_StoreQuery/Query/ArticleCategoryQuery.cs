using _01_StoreQuery.Contracts.Article;
using _01_StoreQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _01_StoreQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _Context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _Context = context;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _Context.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {Id=x.Id,
            Name=x.Name,
            Slug=x.Slug,
            ArticlesCount=x.Articles.Count()
            }).ToList();
        }
        public ArticleCategoryQueryModel GetArticleCategoryWithArticles(string Slug) 
        {

            var Articlecategory = _Context.ArticleCategories
                .Include(x => x.Articles)
                .ThenInclude(x => x.Category)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Articles = MapArticles(x.Articles),

                }).FirstOrDefault(x => x.Slug == Slug);

            if (!string.IsNullOrWhiteSpace(Articlecategory.Keywords))
            {
                Articlecategory.KeyWordList = Articlecategory.Keywords.Split(",").ToList();
            }

            return Articlecategory;
        }
        public static List<ArticleQueryModel> MapArticles(List<Article> articles) 
        {
            var result = new List<ArticleQueryModel>();
            foreach (var article in articles)
            {
                var item = new ArticleQueryModel()
                {
                    Id = article.Id,
                    Picture = article.Picture,
                    PictureAlt = article.PictureAlt,
                    PictureTitle = article.PictureTitle,
                    Title = article.Title,
                    CategoryName = article.Category.Name,
                    Slug = article.Slug
                };
                result.Add(item);
            }
            return result;
        }
    }
}
