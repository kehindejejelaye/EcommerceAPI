namespace Ecommerce.API.Contracts;

public interface IRepositoryManager
{
    Task SaveAsync();
    public ICategoryRepository Category { get; }
    public IProductRepository Product { get; }
    public IVariantRepository Variant { get; }
    public IProductItemRepository ProductItem { get; }
    public IVariantOptionRepository VariantOption { get; }
    public IShoppingCartItemRepository ShoppingCartItem { get; }
    public IAddressRepository Address { get; }
    public IFavoriteRepository Favorite { get; }
    public IWishListRepository WishList { get; }
}
