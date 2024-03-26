using _01_StoreQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _ProductQuery;

        public CartModel(IProductQuery productQuery)
        {
            _ProductQuery = productQuery;
        }

        public void OnGet()
        {
            var Serializar = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems=Serializar.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.unitPrice * item.count;
            }
            CartItems = _ProductQuery.CheckInventoryStatus(cartItems);
        }
        public IActionResult OnGetRemoveFromCart(long id) 
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("/Cart");

        }
        public IActionResult OnGetCheckOut()
        {
            var Serializar = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = Serializar.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            { 
               item.TotalItemPrice= item.unitPrice * item.count;
            }
            CartItems = _ProductQuery.CheckInventoryStatus(cartItems);
            return (RedirectToPage(CartItems.Any(x => !x.IsInStock)? "/Cart" : "/Check"));
        }




    }
}
