using Ecommerce.API.Data;
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
}
