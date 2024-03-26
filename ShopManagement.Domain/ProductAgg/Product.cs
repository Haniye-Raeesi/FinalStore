using _0_FrameWork.Domain;
using CommentManagement.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product:EntityBase
    {
        public string Name { get; private set; }
        public string Code { get;private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescrioption { get; private set; }
        public string Slug { get; private set; }
        public long CategoryId { get;private set; }
        public ProductCategory Category { get;private set; }
        public List<ProductPicture>ProductPictures { get; private set; }
        public List<Comment> Comments { get; private set; }
        protected Product()
        {

        }
        public Product(string name,string code,string description, string picture, string pictureAlt,
        string pictureTitle, string keyWords, string metaDescrioption, string slug,
        long categoryId)
        {
            Name = name;
            Code = code;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescrioption = metaDescrioption;
            Slug = slug;
            CategoryId = categoryId;
        }

        public void Edit(string name, string code, string description, string picture, string pictureAlt,
        string pictureTitle, string keyWords, string metaDescrioption, string slug,
        long categoryId)
        {
            Name = name;
            Code = code;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescrioption = metaDescrioption;
            Slug = slug;
            CategoryId = categoryId;
        }
        
    }

}
