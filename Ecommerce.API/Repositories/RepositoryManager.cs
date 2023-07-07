using Ecommerce.API.Contracts;
using Ecommerce.API.Data;

namespace Ecommerce.API.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly EcommerceContext _context;

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IVariantRepository _variantRepository;


    public RepositoryManager(EcommerceContext context, ICategoryRepository categoryRepository, IProductRepository productRepository, IVariantRepository variantRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _variantRepository = variantRepository;
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
}
