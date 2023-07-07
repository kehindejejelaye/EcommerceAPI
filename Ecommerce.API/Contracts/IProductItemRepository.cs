using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts
{
    public interface IProductItemRepository
    {
        void CreateProductItem(ProductItem productItem);
        void DeleteProductItem(ProductItem productItem);
        Task<IEnumerable<ProductItem>> GetAllProductItemsInProduct(string categoryId, string productId, bool trackChanges);
        Task<ProductItem?> GetProductItemByIdWithCategoryIdAndProductId(string categoryId, string productId, string productItemId, bool trackChanges);
        void UpdateProductItem(ProductItem productItem);
    }
}