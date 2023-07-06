using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
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

        public async Task<IEnumerable<Category>> GetAllCategories(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
