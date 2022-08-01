namespace TangyWeb.Client.Service
{
    using Blazored.LocalStorage;
    using System;
    using Tangy.Common;
    using ViewModel;
    public class CartService : ICartService
    {
        ILocalStorageService _storageService;
        public event Action OnChange;

        public CartService(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task DecrementCart(ShoppingCart shoppingCart)
        {
            var cart = await _storageService.GetItemAsync<ICollection<ShoppingCart>>(Constants.ShoppingCart) as List<ShoppingCart>;
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == shoppingCart.ProductId && cart[i].ProductPriceId == shoppingCart.ProductPriceId)
                {
                    if (cart[i].Count is 1 or 0 || shoppingCart.Count is 0)                    
                        cart.Remove(cart[i]);                    
                    else                    
                        cart[i].Count -= shoppingCart.Count;                    
                }
            }
            
            await _storageService.SetItemAsync(Constants.ShoppingCart, cart);
            OnChange.Invoke();
        }

        public async Task IncrementCart(ShoppingCart shoppingCart)
        {
            bool itemInCart = false;
            var cart = await _storageService.GetItemAsync<ICollection<ShoppingCart>>(Constants.ShoppingCart);
            if (cart is null)            
                cart = new List<ShoppingCart>();

            foreach (var item in cart)
            {
                if (item.ProductId == shoppingCart.ProductId && item.ProductPriceId == shoppingCart.ProductPriceId)
                {
                    itemInCart = true;
                    item.Count += shoppingCart.Count;
                }
            }
            if (!itemInCart)
                cart.Add(new ShoppingCart()
                {
                    ProductId = shoppingCart.ProductId,
                    ProductPriceId = shoppingCart.ProductPriceId,
                    Count = shoppingCart.Count
                });

            await _storageService.SetItemAsync(Constants.ShoppingCart, cart);
            OnChange.Invoke();
        }
    }
}
