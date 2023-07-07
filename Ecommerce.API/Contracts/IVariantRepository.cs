using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts
{
    public interface IVariantRepository
    {
        void CreateVariant(Variant variant);
        void DeleteVariant(Variant variant);
        Task<IEnumerable<Variant>> GetAllVariantsInACategory(string categoryId, bool trackChanges);
        Task<Variant?> GetVariantById(string categoryId, string variantId, bool trackChanges);
        void UpdateVariant(Variant variant);
        Task<bool> VariantExistsAsync(string categoryId, string variantId, bool trackChanges);
    }
}