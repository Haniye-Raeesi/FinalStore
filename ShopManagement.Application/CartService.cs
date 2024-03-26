using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Application
{
    public class CartService : ICartService
    {
        public Cart cartt { get; set; }
        public Cart Get()
        {
            return cartt;
        }

        public void Set(Cart cart)
        {
            cartt = cart;
        }
    }
}
