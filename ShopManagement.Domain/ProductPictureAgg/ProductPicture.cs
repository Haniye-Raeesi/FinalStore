﻿using _0_FrameWork.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture: EntityBase
    {
        public long ProductId { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }
        public Product product { get; private set; }

        public ProductPicture(long productId, string picture,
            string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }
        public void Edit(long productId, string picture,
            string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture; PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            this.product = product;
        }
        public void Remove()
        {
            IsRemoved = true;
        }
        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
