using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface IFavoriteRepository
{
    void AddFavorite(Favorite favorite);
    void DeleteFavorite(Favorite favorite);
    Task<Favorite> GetFavoriteById(string favoriteId, bool trackChanges);
    Task<IEnumerable<Favorite>> GetFavoritesByUserId(string userId, bool trackChanges);
    void UpdateFavorite(Favorite favorite);
}