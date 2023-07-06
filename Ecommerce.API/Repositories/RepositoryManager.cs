using Ecommerce.API.Contracts;
using Ecommerce.API.Data;

namespace Ecommerce.API.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly EcommerceContext _context;

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public RepositoryManager(EcommerceContext context, ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public Task SaveAsync() => _context.SaveChangesAsync();

    public ICategoryRepository Category { 
        get { return _categoryRepository; } 
    }

    public IProductRepository Product
    {
        get { return _productRepository; }
    }
}
