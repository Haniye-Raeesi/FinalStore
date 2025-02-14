﻿using _01_StoreQuery.Contracts.Comment;
using System.Collections.Generic;

namespace _01_StoreQuery.Contracts.Article
{
    public class ArticleQueryModel
    {
        public long Id { get; set; }
        public string PublishDate { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string CategorySlug { get; set; }
        public string CanonicalAddress { get; set; }
        public List<string> KeyWordsList { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
    }
}
