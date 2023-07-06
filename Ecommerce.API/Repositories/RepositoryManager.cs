using Ecommerce.API.Contracts;
using Ecommerce.API.Data;

namespace Ecommerce.API.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly EcommerceContext _context;

    private readonly ICategoryRepository _categoryRepository;

    public RepositoryManager(EcommerceContext context, ICategoryRepository categoryRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
    }

    public Task SaveAsync() => _context.SaveChangesAsync();

    public ICategoryRepository Category { 
        get { return _categoryRepository; } 
    }
}
