using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts
{
    public interface IVariantOptionRepository
    {
        void CreateVariantOption(VariantOption variantOption);
        void DeleteVariantOption(VariantOption variantOption);
        Task<IEnumerable<VariantOption>> GetAllVariantOptionsInVariant(string variantId, bool trackChanges);
        Task<VariantOption?> GetVariantOptionById(string variantId, string variantOptionId, bool trackChanges);
        void UpdateVariantOption(VariantOption variantOption);
    }
}