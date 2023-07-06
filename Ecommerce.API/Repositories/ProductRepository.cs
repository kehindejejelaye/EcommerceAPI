﻿using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async Task<Product?> GetProductById(string categoryId, string productId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id == productId && p.CategoryId == categoryId, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProducts(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);
    public void UpdateProduct(Product product) => Update(product);
}
