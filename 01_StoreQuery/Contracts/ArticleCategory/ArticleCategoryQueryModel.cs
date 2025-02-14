﻿using _01_StoreQuery.Contracts.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_StoreQuery.Contracts.ArticleCategory
{
   public class ArticleCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Description { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
        public int ArticlesCount { get; set; }
        public List<string> KeyWordList { get; set; }
    }
}
