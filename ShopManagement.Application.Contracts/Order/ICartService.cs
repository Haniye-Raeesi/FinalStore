namespace ShopManagement.Application.Contracts.Order
{
    public interface ICartService
    {
        public Cart Get();
        public void Set(Cart cart);
    }
}
