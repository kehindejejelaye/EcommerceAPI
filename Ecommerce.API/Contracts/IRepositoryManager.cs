namespace Ecommerce.API.Contracts;

public interface IRepositoryManager
{
    Task SaveAsync();
    public ICategoryRepository Category { get; }
    public IProductRepository Product { get; }
    public IVariantRepository Variant { get; }
    public IProductItemRepository ProductItem { get; }
}
