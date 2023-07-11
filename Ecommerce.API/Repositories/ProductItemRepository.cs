using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.DTOs.Product;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
using Ecommerce.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class ProductItemRepository : BaseRepository<ProductItem>, IProductItemRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;

        public ProductItemRepository(EcommerceContext _ecomContext, IPropertyMappingService propertyMappingService) : base(_ecomContext)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<ProductItem?> GetProductItemByIdWithCategoryIdAndProductId(string categoryId, string productId, string productItemId, bool trackChanges)
        {
            return await FindByCondition(pi => pi.Id == productItemId && pi.ProductId == productId && pi.Product.CategoryId == categoryId, trackChanges)
                .Include(pi => pi.Product)
                .SingleOrDefaultAsync();
        }

        public async Task<ProductItem?> GetProductItemById(string productItemId, bool trackChanges)
        {
            return await FindByCondition(pi => pi.Id == productItemId, trackChanges)
                .Include(pi => pi.Product)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> DoesProductItemExist(string productItemId, bool trackChanges)
        {
            var product = await FindByCondition(pi => pi.Id == productItemId, trackChanges).SingleOrDefaultAsync();

            return product != null;
        }

        public async Task<IEnumerable<ProductItem>> GetAllProductItemsInProduct(RequestParameters requestParameters, string categoryId, string productId, bool trackChanges)
        {
            var collection = FindByCondition(pi => pi.ProductId == productId && pi.Product.CategoryId == categoryId, trackChanges);

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
            if (!string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                // get property mapping dictionary
                var categoryPropertyMappingDictionary = _propertyMappingService
                    .GetPropertyMapping<ReadProductDto, Product>();

                collection = collection.ApplySort(requestParameters.OrderBy,
                    categoryPropertyMappingDictionary);
            }

            return await PagedList<ProductItem>.CreateAsync(collection, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public void CreateProductItem(ProductItem productItem) => Create(productItem);

        public void DeleteProductItem(ProductItem productItem) => Delete(productItem);

        public void UpdateProductItem(ProductItem productItem) => Update(productItem);
    }
}
