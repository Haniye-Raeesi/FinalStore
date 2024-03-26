using _0_FrameWork.InfraStructure;
using _01_StoreQuery.Contracts.Product;
using _01_StoreQuery.Contracts.ProductCategory;
using _01_StoreQuery.Contracts.Slide;
using _01_StoreQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.InfraStructure.Configuration.Permission;
using ShopManagement.InfraStructure.EfCore;
using ShopManagement.InfraStructure.EfCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection Services,string connectionString)
        {
            Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            Services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            Services.AddTransient<IProductRepository, ProductRepository>();
            Services.AddTransient<IProductApplication, ProductApplication>();

            Services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            Services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            Services.AddTransient<ISlideRepository, SlideRepository>();
            Services.AddTransient<ISlideApplication, SlideApplication>();

            Services.AddTransient<ISlideQuery, SlideQuery>();
            Services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            Services.AddTransient<IProductQuery, ProductQuery>();
            Services.AddTransient<IPermissionExposer, ShopPermissionExposer>();
            Services.AddSingleton<ICartService, CartService>();
            Services.AddTransient<IOrderRepository, OrderRepository>();
            Services.AddTransient<IOrderApplication, OrderApplication>();



            //Services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
            Services.AddDbContext<ShopContext>(
            options => options.UseSqlServer(
            (connectionString),
            providerOptions => providerOptions.EnableRetryOnFailure()));

        }
    }
}
