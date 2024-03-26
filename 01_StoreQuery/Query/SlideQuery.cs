using _01_StoreQuery.Contracts.Slide;
using ShopManagement.InfraStructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_StoreQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _Context;

        public SlideQuery(ShopContext context)
        {
            _Context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _Context.Slides.Select(x=>new SlideQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Link = x.Link,
                Text = x.Text,
                Title = x.Title
            }  ).ToList();
        }
    }
}
