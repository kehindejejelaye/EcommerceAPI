using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ShoppingCartItemRepository : BaseRepository<ShoppingCartItem>, IShoppingCartItemRepository
{
    public ShoppingCartItemRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async void AddToCart(ShoppingCartItem item)
    {
        var shoppingCartItem = await FindByCondition(_item => item.UserId == _item.UserId && item.ProductItemId == _item.ProductItemId, trackChanges: true).SingleOrDefaultAsync();

        if (shoppingCartItem == null)
        {
            Create(item);
            return;
        }

        shoppingCartItem.Quantity += item.Quantity;
    }

    public async void RemoveFromCart(ShoppingCartItem item)
    {
        var shoppingCartItem = await FindByCondition(_item => item.UserId == _item.UserId && item.ProductItemId == _item.ProductItemId, trackChanges: true).SingleOrDefaultAsync();

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Quantity > 1)
            {
                shoppingCartItem.Quantity -= item.Quantity;
            }
            else
            {
                Delete(shoppingCartItem);
            }
        }
    }

    public async Task<List<ShoppingCartItem>> GetShoppingCartItems(string userId)
    {
        return await FindByCondition(c => c.UserId == userId, trackChanges: false)
            .Include(c => c.ProductItem)
            .ToListAsync();
    }

    public async Task<ShoppingCartItem?> GetShoppingCartItemByProductItemId(string userId, string productItemId)
    {
        return await FindByCondition(c => c.UserId == userId && c.ProductItemId == productItemId, trackChanges: false).SingleOrDefaultAsync();
    }

    public void ClearShoppingCart(string userId)
    {
        var cartItems = FindByCondition(c => c.UserId == userId, trackChanges: false)
            .ToList();

        DeleteMultiple(cartItems);
    }

    public decimal GetShoppingCartTotal(string userId)
    {
        var cartItems = FindByCondition(c => c.UserId == userId, trackChanges: false)
        .Include(c => c.ProductItem)
        .ToList();

        var total = cartItems
            .Sum(c => (double)c.Quantity * (double)c.ProductItem.Price);

        return (decimal)Math.Round(total, 2);
    }

        public async Task<Order> PopulateOrderWithItems(string userId)
    {
        var cartItems = await FindByCondition(c => c.UserId == userId, trackChanges: false)
            .Include(c => c.ProductItem)
            .ToListAsync();

        var orderStatusId = "e1dce2c9-9681-4d62-bc54-5e0e8eac6492";

        // Create the order and set its properties
        var order = new Order
        {
            OrderTotal = cartItems.Sum(c => c.Quantity * c.ProductItem.Price),
            OrderStatusId = orderStatusId,
            UserId = userId,
            OrderItems = cartItems.Select(c => new OrderItem
            {
                ProductItemId = c.ProductItemId,
                Price = c.ProductItem.Price,
                Quantity = c.Quantity
            }).ToList()
        };

        
        return order;
    }
}
