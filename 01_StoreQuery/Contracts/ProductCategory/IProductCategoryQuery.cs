using System.Collections.Generic;

namespace _01_StoreQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        public List<ProductCategoryQueryModel> GetProductCategories();
        public List<ProductCategoryQueryModel> GetCategoryWithProducts();
        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string Slug);
    }
}
