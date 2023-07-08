using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;

namespace Ecommerce.API.Contracts;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories(RequestParameters requestParameters, bool trackChanges);
    Task<Category?> GetCategoryById(string categoryId, bool trackChanges);
    void CreateCategory(Category category);

    void DeleteCategory(Category category);
    void UpdateCategory(Category category);
    Task<bool> CategoryExistsAsync(string categoryId, bool trackChanges);
}
