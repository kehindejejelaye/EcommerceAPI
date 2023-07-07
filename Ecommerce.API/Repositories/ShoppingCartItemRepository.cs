﻿using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ShoppingCartItemRepository : BaseRepository<ShoppingCartItem>
{
    public ShoppingCartItemRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async void AddToCart(ShoppingCartItem item)
    {
        var shoppingCartItem = await FindByCondition(_item => item.UserId == _item.UserId && item.ProductItemId == _item.ProductItemId, trackChanges: false).SingleOrDefaultAsync();

        if (shoppingCartItem == null)
        {
            Create(item);
            return;
        }

        shoppingCartItem.Quantity += item.Quantity;
    }

    public async void RemoveFromCart(ShoppingCartItem item)
    {
        var shoppingCartItem = await FindByCondition(_item => item.UserId == _item.UserId && item.ProductItemId == _item.ProductItemId, trackChanges: false).SingleOrDefaultAsync();

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

    public List<ShoppingCartItem> GetShoppingCartItems(string userId)
    {
        return FindByCondition(c => c.UserId == userId, trackChanges: false)
            .Include(c => c.ProductItem)
            .ToList();
    }

    public void ClearShoppingCart(string userId)
    {
        var cartItems = FindByCondition(c => c.UserId == userId, trackChanges: false)
            .ToList();

        DeleteMultiple(cartItems);
    }


}