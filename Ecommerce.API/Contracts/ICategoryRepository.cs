using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories(bool trackChanges);
    Category? GetCategoryById(string categoryId, bool trackChanges);
}
