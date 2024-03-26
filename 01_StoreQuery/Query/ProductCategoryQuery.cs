using _0_FrameWork.Application;
using _01_StoreQuery.Contracts.Product;
using _01_StoreQuery.Contracts.ProductCategory;
using DiscountManagement.InfraStructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.InfraStructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace _01_StoreQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _Context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _Context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }
        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _Context.ProductCategories
       .Select(x => new ProductCategoryQueryModel
       {
           Id = x.Id,
           Name = x.Name,
           Picture = x.Picture,
           PictureAlt = x.PictureAlt,
           PictureTitle = x.PictureTitle,
           Slug = x.Slug,

       })
       .ToList();
        }
            public List<ProductCategoryQueryModel> GetCategoryWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
               .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var categories = _Context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Products = MapProducts(x.Products)

                })
                .ToList();
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();

                        var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                        if (productDiscount != null)
                        {
                            int prDis = productDiscount.DiscountRate;
                            var DisAmount = Math.Round(price * prDis) / 100;
                            product.PriceWithDiscount = (price - DisAmount).ToMoney();
                            product.HasDiscount = productDiscount.DiscountRate > 0;
                        }

                    }


                }

            }
            return categories;

        }
        public static List<ProductQueryModel>  MapProducts(List<Product> products)
        {
            var result = new List<ProductQueryModel>();
            foreach (var product in products)
            {
                var item = new ProductQueryModel()
                {
                    Id = product.Id,
                    Picture=product.Picture,
                    PictureAlt=product.PictureAlt,
                    PictureTitle=product.PictureTitle,
                    Name=product.Name,
                    Category=product.Category.Name,
                    Slug=product.Slug
                };
                result.Add(item);
            }
            return result;
 
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string Slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
               .Select(x => new { x.DiscountRate, x.ProductId , x.EndDate}).ToList();

            var category = _Context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Description=x.Description,
                    MetaDescription=x.MetaDescrioption,
                    Products = MapProducts(x.Products)

                }).FirstOrDefault(x=>x.Slug==Slug);

                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();

                        var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                        if (productDiscount != null)
                        {
                        product.DiscountRate = productDiscount.DiscountRate;
                            int prDis = productDiscount.DiscountRate;
                            var DisAmount = Math.Round(price * prDis) / 100;
                            product.PriceWithDiscount = (price - DisAmount).ToMoney();
                            product.HasDiscount = productDiscount.DiscountRate > 0;
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();


                        }
                    }
                } 
            return category;
        }
    }
}
