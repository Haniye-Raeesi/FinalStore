using _01_StoreQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class BlogPostsViewComponent:ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public BlogPostsViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }
        public IViewComponentResult Invoke()
        {
            var Articles = _articleQuery.GetArticles();
            return View(Articles);
        }
    }
}
