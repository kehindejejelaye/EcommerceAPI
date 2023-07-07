using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface IProductRepository
{
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
    Task<IEnumerable<Product>> GetAllProductsInACategory(string categoryId, bool trackChanges);
    Task<Product?> GetProductById(string categoryId, string productId, bool trackChanges);
    void UpdateProduct(Product product);
    Task<bool> ProductExistsAsync(string categoryId, string productId, bool trackChanges);
}
