using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.InfraStructure.EfCore.Repository
{
    public class ProductCategoryRepository :RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _Context;

        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _Context = context;
        }

        public EditProductCategory GetDetails(long id)
        {
            return _Context.ProductCategories.Select(x => new EditProductCategory
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                KeyWords = x.KeyWords,
                MetaDescrioption = x.MetaDescrioption,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _Context.ProductCategories.Select(x=>new ProductCategoryViewModel { 
            Id=x.Id,
            Name=x.Name}).
            ToList();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _Context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            return query.OrderByDescending(x => x.Id).ToList();
            
        }
    }
}
