using Ecommerce.API.Contracts;
using Ecommerce.API.Data;

namespace Ecommerce.API.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly EcommerceContext _context;

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IVariantRepository _variantRepository;
    private readonly IProductItemRepository _piRepository;
    private readonly IVariantOptionRepository _voRepository;
    private readonly IShoppingCartItemRepository _scRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IWishListRepository _wishListRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IOrderRepository _orderRepository;
    public RepositoryManager(EcommerceContext context, ICategoryRepository categoryRepository, IProductRepository productRepository, IVariantRepository variantRepository, IProductItemRepository piRepository, IVariantOptionRepository voRepository, IShoppingCartItemRepository scRepository, IAddressRepository addressRepository, IFavoriteRepository favoriteRepository, IWishListRepository wishListRepository, IReviewRepository reviewRepository, IOrderRepository orderRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _variantRepository = variantRepository;
        _piRepository = piRepository;
        _voRepository = voRepository;
        _scRepository = scRepository;
        _addressRepository = addressRepository;
        _favoriteRepository = favoriteRepository;
        _wishListRepository = wishListRepository;
        _reviewRepository = reviewRepository;
        _orderRepository = orderRepository;
    }

    public Task SaveAsync() => _context.SaveChangesAsync();

    public IOrderRepository Order
    {
        get { return _orderRepository; }
    }

    public ICategoryRepository Category { 
        get { return _categoryRepository; } 
    }

    public IProductRepository Product
    {
        get { return _productRepository; }
    }

    public IVariantRepository Variant
    {
        get { return _variantRepository; }
    }

    public IProductItemRepository ProductItem
    {
        get { return _piRepository; }
    }

    public IVariantOptionRepository VariantOption
    {
        get { return _voRepository; }
    }
    
    public IShoppingCartItemRepository ShoppingCartItem
    {
        get { return _scRepository; }
    }

    public IAddressRepository Address
    {
        get { return _addressRepository; }
    }

    public IFavoriteRepository Favorite
    {
        get { return _favoriteRepository; }
    }

    public IWishListRepository WishList
    {
        get { return _wishListRepository; }
    }

    public IReviewRepository Review
    {
        get { return _reviewRepository; }
    }
}
