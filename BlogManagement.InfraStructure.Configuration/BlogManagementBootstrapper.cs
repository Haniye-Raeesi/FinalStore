using _01_StoreQuery.Contracts.Article;
using _01_StoreQuery.Contracts.ArticleCategory;
using _01_StoreQuery.Query;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EfCore;
using BlogManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection Services, String ConnectionString) 
        {
            Services.AddTransient<IArticleApplication,ArticleApplication>();
            Services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            Services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            Services.AddTransient<IArticleRepository, ArticleRepository>();
            Services.AddTransient<IArticleQuery, ArticleQuery>();
            Services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();


            Services.AddDbContext<BlogContext>(
            options => options.UseSqlServer(
            (ConnectionString),
            providerOptions => providerOptions.EnableRetryOnFailure()));

        }

    }
}
