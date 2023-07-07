using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async Task<Product?> GetProductById(string categoryId, string productId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id == productId && p.CategoryId == categoryId, trackChanges).Include(p => p.Category)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsInACategory(string categoryId, bool trackChanges)
    {
        return await FindByCondition(p =>p.CategoryId == categoryId, trackChanges)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);
    public void UpdateProduct(Product product) => Update(product);

    public async Task<bool> ProductExistsAsync(string categoryId, string productId, bool trackChanges)
    {
        var product = await FindByCondition(p => p.Id == productId && p.CategoryId == categoryId, trackChanges).FirstOrDefaultAsync();
        return product != null;
    }
}
