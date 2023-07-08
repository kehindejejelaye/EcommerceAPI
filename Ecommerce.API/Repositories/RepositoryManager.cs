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


    public RepositoryManager(EcommerceContext context, ICategoryRepository categoryRepository, IProductRepository productRepository, IVariantRepository variantRepository, IProductItemRepository piRepository, IVariantOptionRepository voRepository, IShoppingCartItemRepository scRepository, IAddressRepository addressRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _variantRepository = variantRepository;
        _piRepository = piRepository;
        _voRepository = voRepository;
        _scRepository = scRepository;
        _addressRepository = addressRepository;
    }

    public Task SaveAsync() => _context.SaveChangesAsync();

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
}
