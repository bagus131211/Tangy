namespace TangyWeb.Client.Service
{
    using ViewModel;
    public interface ICartService
    {
        event Action OnChange;
        Task DecrementCart(ShoppingCart shoppingCart);
        Task IncrementCart(ShoppingCart shoppingCart);

    }
}
