using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.InfraStructure.EfCore.Repository
{
   public class ProductRepository: RepositoryBase<long,Product>,IProductRepository
    {
        private readonly ShopContext _Context;

        public ProductRepository(ShopContext context):base(context)
        {
            _Context = context;
        }

        public EditProduct GetDetails(long Id)
        {
            return _Context.Products.Select(x=> new EditProduct 
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                Description = x.Description,
                KeyWords = x.KeyWords,
                MetaDescrioption = x.MetaDescrioption,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
            }).FirstOrDefault(x=>x.Id==Id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _Context.Products.Select(x=> new ProductViewModel 
            {Id=x.Id,
            Name=x.Name,            
            }).ToList();
        }

        public Product GetProductWithCategory(long Id)
        {
            return _Context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == Id);
        }

        public List<ProductViewModel> Search(ProductSearchModel SearchModel)
        {
            var query = _Context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
            {
                Id=x.Id,
                Name=x.Name,
                Picture=x.Picture,
                Code=x.Code,
                Category=x.Category.Name,
                CategoryId=x.CategoryId,
                CreationDate=x.CreationDate.ToFarsi()
            }).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(SearchModel.Name))
                query = query.Where(x => x.Name.Contains(SearchModel.Name));
            if (!string.IsNullOrWhiteSpace(SearchModel.Code))
                query = query.Where(x => x.Code.Contains(SearchModel.Code));
            if (SearchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == SearchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
