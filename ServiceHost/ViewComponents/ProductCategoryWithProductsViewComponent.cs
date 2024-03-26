using _01_StoreQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;


namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _productCategoryQuery.GetCategoryWithProducts();
            return View(categories);
        }

    }
}
