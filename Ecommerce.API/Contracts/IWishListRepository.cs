using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts
{
    public interface IWishListRepository
    {
        void AddWishList(WishList wishList);
        void DeleteWishList(WishList wishList);
        Task<WishList?> GetWishListForUserById(string userId, string wishListId, bool trackChanges);
        Task<IEnumerable<WishList>> GetWishListsForUserByUserId(string userId, bool trackChanges);
        void UpdateWishList(WishList wishList);
    }
}