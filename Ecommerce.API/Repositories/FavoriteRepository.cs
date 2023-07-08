using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(EcommerceContext ecommerceContext) : base(ecommerceContext)
    {
    }

    public async Task<Favorite?> GetFavoriteById(string favoriteId, bool trackChanges)
    {
        return await FindByCondition(f => f.Id == favoriteId, trackChanges)
            .Include(f => f.ProductItem)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Favorite>> GetFavoritesByUserId(string userId, bool trackChanges)
    {
        return await FindByCondition(f => f.UserId == userId, trackChanges)
            .Include(f => f.ProductItem)
            .ToListAsync();
    }

    public void AddFavorite(Favorite favorite) => Create(favorite);

    public void UpdateFavorite(Favorite favorite) => Update(favorite);

    public void DeleteFavorite(Favorite favorite) => Delete(favorite);
}
