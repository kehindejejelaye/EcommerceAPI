using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class VariantRepository : BaseRepository<Variant>, IVariantRepository
{
    public VariantRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async Task<Variant?> GetVariantById(string categoryId, string variantId, bool trackChanges)
    {
        return await FindByCondition(v => v.Id == variantId && v.CategoryId == categoryId, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Variant>> GetAllVariantsInACategory(string categoryId, bool trackChanges)
    {
        return await FindByCondition(v => v.CategoryId == categoryId, trackChanges)
            .OrderBy(v => v.Name)
            .ToListAsync();
    }

    public void CreateVariant(Variant variant) => Create(variant);

    public void UpdateVariant(Variant variant) => Update(variant);

    public void DeleteVariant(Variant variant) => Delete(variant);

    public async Task<bool> VariantExistsAsync(string categoryId, string variantId, bool trackChanges)
    {
        var variant = await FindByCondition(p => p.Id == variantId && p.CategoryId == categoryId, trackChanges).FirstOrDefaultAsync();
        return variant != null;
    }
}
