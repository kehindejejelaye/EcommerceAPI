using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class ProductItemRepository : BaseRepository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(EcommerceContext _ecomContext) : base(_ecomContext)
        {
        }

        public async Task<ProductItem?> GetProductItemById(string categoryId, string productId, string productItemId, bool trackChanges)
        {
            return await FindByCondition(pi => pi.Id == productItemId && pi.ProductId == productId && pi.Product.CategoryId == categoryId, trackChanges)
                .Include(pi => pi.Product)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductItem>> GetAllProductItemsInProduct(string categoryId, string productId, bool trackChanges)
        {
            return await FindByCondition(pi => pi.ProductId == productId && pi.Product.CategoryId == categoryId, trackChanges)
                .OrderBy(pi => pi.SKU)
                .ToListAsync();
        }

        public void CreateProductItem(ProductItem productItem) => Create(productItem);

        public void DeleteProductItem(ProductItem productItem) => Delete(productItem);

        public void UpdateProductItem(ProductItem productItem) => Update(productItem);
    }
}
