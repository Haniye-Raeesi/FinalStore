using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.InfraStructure.EfCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _Context;

        public SlideRepository(ShopContext context):base(context)
        {
            _Context = context;
        }

        public EditSlide GetDetails(long Id)
        {
            return _Context.Slides.Select(x=> new EditSlide 
            {
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Link = x.Link,
                Title = x.Title
            }).FirstOrDefault(x=>x.Id==Id);
        }

        public List<SlideViewModel> GetList()
        {
            return _Context.Slides.Select(x=>new SlideViewModel
                {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x=>x.Id).ToList();
        }
    }
}
