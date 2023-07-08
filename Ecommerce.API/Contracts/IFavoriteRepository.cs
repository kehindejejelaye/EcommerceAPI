using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface IFavoriteRepository
{
    void AddFavorite(Favorite favorite);
    void DeleteFavorite(Favorite favorite);
    Task<Favorite> GetFavoriteForUserById(string userId, string favoriteId, bool trackChanges);
    Task<IEnumerable<Favorite>> GetFavoritesForUserByUserId(string userId, bool trackChanges);
    void UpdateFavorite(Favorite favorite);
}