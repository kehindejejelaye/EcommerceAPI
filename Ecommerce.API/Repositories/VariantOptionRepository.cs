using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class VariantOptionRepository : BaseRepository<VariantOption>, IVariantOptionRepository
{
    public VariantOptionRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async Task<VariantOption?> GetVariantOptionById(string variantId, string variantOptionId, bool trackChanges)
    {
        return await FindByCondition(vo => vo.Id == variantOptionId && vo.VariantId == variantId, trackChanges)
            .Include(vo => vo.Variant)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<VariantOption>> GetAllVariantOptionsInVariant(string variantId, bool trackChanges)
    {
        return await FindByCondition(vo => vo.VariantId == variantId, trackChanges)
            .OrderBy(vo => vo.Value)
            .ToListAsync();
    }

    public void CreateVariantOption(VariantOption variantOption) => Create(variantOption);

    public void DeleteVariantOption(VariantOption variantOption) => Delete(variantOption);

    public void UpdateVariantOption(VariantOption variantOption) => Update(variantOption);
}
