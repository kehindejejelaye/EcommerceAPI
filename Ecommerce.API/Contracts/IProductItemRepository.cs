using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;

namespace Ecommerce.API.Contracts
{
    public interface IProductItemRepository
    {
        void CreateProductItem(ProductItem productItem);
        void DeleteProductItem(ProductItem productItem);
        Task<IEnumerable<ProductItem>> GetAllProductItemsInProduct(RequestParameters requestParameters, string categoryId, string productId, bool trackChanges);
        Task<ProductItem?> GetProductItemByIdWithCategoryIdAndProductId(string categoryId, string productId, string productItemId, bool trackChanges);
        Task<ProductItem?> GetProductItemById(string productItemId, bool trackChanges);
        void UpdateProductItem(ProductItem productItem);
        Task<bool> DoesProductItemExist(string productItemId, bool trackChanges);
    }
}