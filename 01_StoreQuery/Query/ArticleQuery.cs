using _0_FrameWork.Application;
using _01_StoreQuery.Contracts.Article;
using _01_StoreQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_StoreQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _BlogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _BlogContext = blogContext;
            _commentContext = commentContext;
        }

        public List<ArticleQueryModel> GetArticles()
        {
            return _BlogContext.Articles
                .Include(x=>x.Category)
                .Where(x=>x.PublishDate<DateTime.Now)
                .Select(x => new ArticleQueryModel {
            Title=x.Title,
            Picture=x.Picture,
            PictureAlt=x.PictureAlt,
            PictureTitle=x.PictureTitle,
            Slug=x.Slug,
            PublishDate=x.PublishDate.ToFarsi()
            }).ToList();
        }

        public ArticleQueryModel GetDetails(string slug)
        {

            var article = _BlogContext.Articles.Select(x => new ArticleQueryModel
            {
                Id=x.Id,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                PublishDate = x.PublishDate.ToFarsi(),
                CategoryName = x.Category.Name,
                Description = x.Description,
                KeyWords = x.Keywords
            }).FirstOrDefault(x => x.Slug == slug);
            if (!string.IsNullOrWhiteSpace(article.KeyWords))
            {
                article.KeyWordsList = article.KeyWords.Split(",").ToList();
            }
            var ArticleComments = _commentContext.Comments
                 .Where(x => !x.IsCanceled)
                 .Where(x => x.IsConfirmed)
                 .Where(x => x.OwnerRecordId == article.Id)
                 .Where(x => x.Type == 2)
                 .Select(x => new CommentQueryModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Message = x.Message,
                     ParentId=x.ParentId,
                     ParentName=x.ParentComment.Name,
                     CreationDate = x.CreationDate.ToFarsi(),
                 }).ToList();
            foreach (var comment in ArticleComments)
            {
                if (comment.ParentId>0)
                {
                    comment.ParentName = ArticleComments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                }

            }
            article.Comments = ArticleComments;

            return article;

            //var article = _BlogContext.Articles
            //   .Include(x => x.Category)
            //   .Where(x => x.PublishDate <= DateTime.Now)
            //   .Select(x => new ArticleQueryModel
            //   {
            //       Id = x.Id,
            //       Title = x.Title,
            //       CategoryName = x.Category.Name,
            //       CategorySlug = x.Category.Slug,
            //       Slug = x.Slug,
            //       CanonicalAddress = x.CanonicalAddress,
            //       Description = x.Description,
            //       KeyWords = x.Keywords,
            //       MetaDescription = x.MetaDescription,
            //       Picture = x.Picture,
            //       PictureAlt = x.PictureAlt,
            //       PictureTitle = x.PictureTitle,
            //       PublishDate = x.PublishDate.ToFarsi(),
            //       ShortDescription = x.ShortDescription,
            //   }).FirstOrDefault(x => x.Slug == slug);

            //if (!string.IsNullOrWhiteSpace(article.KeyWords))
            //    article.KeyWordsList = article.KeyWords.Split(",").ToList();


            //var comments = _commentContext.Comments
            //    .Where(x => !x.IsCanceled)
            //    .Where(x => x.IsConfirmed)
            //    .Where(x => x.Type == CommentType.Article)
            //    .Where(x => x.OwnerRecordId == article.Id)
            //    .Select(x => new CommentQueryModel
            //    {
            //        Id = x.Id,
            //        Message = x.Message,
            //        Name = x.Name,
            //        ParentId = x.ParentId,
            //        CreationDate = x.CreationDate.ToFarsi()
            //    }).OrderByDescending(x => x.Id).ToList();

            //foreach (var comment in comments)
            //{
            //    if (comment.ParentId > 0)
            //        comment.parentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            //}

            //article.Comments = comments;

            //return article;

        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _BlogContext.Articles
                .Where(x => x.PublishDate < DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    Title = x.Title,
                    PublishDate = x.PublishDate.ToFarsi()

                }).ToList();
        }
    }
}
