using _01_StoreQuery;
using _01_StoreQuery.Contracts.ArticleCategory;
using _01_StoreQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IProductCategoryQuery _productCategoryQuary;

        public MenuViewComponent(IArticleCategoryQuery articleCategoryQuery,
            IProductCategoryQuery productCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuary = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var Result = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuary.GetProductCategories()

            };
            return View(Result);
        }


    }
}




    
