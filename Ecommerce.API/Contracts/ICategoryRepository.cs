using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories(bool trackChanges);
    Task<Category?> GetCategoryById(string categoryId, bool trackChanges);
    void CreateCategory(Category category);

    void DeleteCategory(Category category);
    void UpdateCategory(Category category);
}
