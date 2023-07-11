using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async Task<Category?> GetCategoryById(string categoryId, bool trackChanges)
    {
        return await FindByCondition(c => c.Id == categoryId, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetAllCategories(RequestParameters requestParameters, bool trackChanges)
    {
        var collection = FindAll(trackChanges);

        // filter
        if (!string.IsNullOrWhiteSpace(requestParameters.FilterOnName))
        {
            var filterOnName = requestParameters.FilterOnName.Trim();

            collection = collection.Where(c => c.Name.Contains(filterOnName));
        }

        // search 
        if (!string.IsNullOrWhiteSpace(requestParameters.SearchQuery))
        {
            var searchQuery = requestParameters.SearchQuery.Trim();

            collection = collection.Where(c => c.Name.Contains(searchQuery));
        }

        // sort

        return await PagedList<Category>.CreateAsync(collection,
             requestParameters.PageNumber,
             requestParameters.PageSize);
    }

    public void CreateCategory(Category category) => Create(category);

    public void DeleteCategory(Category category) => Delete(category);
    public void UpdateCategory(Category category) => Update(category);

    public async Task<bool> CategoryExistsAsync(string categoryId, bool trackChanges)
    {
        var category = await FindByCondition(c => c.Id == categoryId, trackChanges).FirstOrDefaultAsync();
        return category != null;
    }
}
