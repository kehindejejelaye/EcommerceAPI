using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;

namespace Ecommerce.API.Contracts;

public interface IProductRepository
{
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
    Task<IEnumerable<Product>> GetAllProductsInACategory(RequestParameters requestParameters, string categoryId, bool trackChanges);
    Task<Product?> GetProductById(string categoryId, string productId, bool trackChanges);
    void UpdateProduct(Product product);
    Task<bool> ProductExistsAsync(string categoryId, string productId, bool trackChanges);
}
