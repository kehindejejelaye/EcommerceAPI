using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.DTOs.Product;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
using Ecommerce.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly IPropertyMappingService _propertyMappingService;

    public ProductRepository(EcommerceContext _ecomContext, IPropertyMappingService propertyMappingService) : base(_ecomContext)
    {
        _propertyMappingService = propertyMappingService;
    }

    public async Task<Product?> GetProductById(string categoryId, string productId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id == productId && p.CategoryId == categoryId, trackChanges).Include(p => p.Category)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsInACategory(RequestParameters requestParameters, string categoryId, bool trackChanges)
    {
        var collection = FindAll(trackChanges);

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

        return await PagedList<Product>.CreateAsync(collection,
             requestParameters.PageNumber,
             requestParameters.PageSize);

    }

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);
    public void UpdateProduct(Product product) => Update(product);

    public async Task<bool> ProductExistsAsync(string categoryId, string productId, bool trackChanges)
    {
        var product = await FindByCondition(p => p.Id == productId && p.CategoryId == categoryId, trackChanges).FirstOrDefaultAsync();
        return product != null;
    }
}
