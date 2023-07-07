using Ecommerce.API.Data;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Repositories;

public class ShoppingCartItemRepository : BaseRepository<ShoppingCartItem>
{
    public ShoppingCartItemRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }
}
