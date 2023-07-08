using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class WishListRepository : BaseRepository<WishList>, IWishListRepository
{
    public WishListRepository(EcommerceContext ecommerceContext) : base(ecommerceContext)
    {
    }

    public async Task<WishList?> GetWishListForUserById(string userId, string wishListId, bool trackChanges)
    {
        return await FindByCondition(w => w.Id == wishListId && w.UserId == userId, trackChanges)
            .Include(w => w.ProductItem)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<WishList>> GetWishListsForUserByUserId(string userId, bool trackChanges)
    {
        return await FindByCondition(w => w.UserId == userId, trackChanges)
            .Include(w => w.ProductItem)
            .ToListAsync();
    }

    public void AddWishList(WishList wishList) => Create(wishList);

    public void UpdateWishList(WishList wishList) => Update(wishList);

    public void DeleteWishList(WishList wishList) => Delete(wishList);
}
