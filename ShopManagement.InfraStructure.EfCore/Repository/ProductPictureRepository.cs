using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.InfraStructure.EfCore.Repository
{
    public class ProductPictureRepository :RepositoryBase<long,ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _Context;

        public ProductPictureRepository(ShopContext context):base(context)
        {
            _Context = context;
        }

        public ProductPicture GetByProductAndCategory(long id)
        {
            return _Context.ProductPictures.Include(x => x.product)
                .ThenInclude(x => x.Category).FirstOrDefault(x=>x.Id==id);
        }

        public EditProductPicture GetDetails(long Id)
        {
          return  _Context.ProductPictures.Select(x => new EditProductPicture
          {     Id = x.Id,
                //Picture = x.Picture,
                ProductId = x.ProductId,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle

            }).FirstOrDefault(x => x.Id == Id);
        }

       

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _Context.ProductPictures
                .Include(x => x.product)
                .Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved,
                Product = x.product.Name
            });
            if (searchModel.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }
            return query.ToList();

        }
        
    }
}
