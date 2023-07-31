using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts
{
    public interface IShoppingCartItemRepository
    {
        void AddToCart(ShoppingCartItem item);
        void ClearShoppingCart(string userId);
        Task<List<ShoppingCartItem>> GetShoppingCartItems(string userId);
        decimal GetShoppingCartTotal(string userId);
        void RemoveFromCart(ShoppingCartItem item);
        Task<ShoppingCartItem?> GetShoppingCartItemByProductItemId(string userId, string productItemId);
        Task<Order> PopulateOrderWithItems(string userId);
    }
}