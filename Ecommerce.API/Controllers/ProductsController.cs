using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Product;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/categories/{categoryId}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepositoryManager _repoManager;
    private readonly IMapper _mapper;

    public ProductsController(IRepositoryManager repoManager, IMapper mapper)
    {
        _repoManager = repoManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProductsInACategory([FromQuery] RequestParameters requestParameters, string categoryId)
    {
        var products = await _repoManager.Product.GetAllProductsInACategory(requestParameters, categoryId, false);
        var _products = _mapper.Map<IEnumerable<ReadProductDto>>(products);
        return Ok(_products);
    }

    [HttpGet("{productId}", Name = "GetSingleProductInCategory")]
    public async Task<ActionResult> GetSingleProductInCategory(string categoryId, string productId)
    {
        if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
        {
            return NotFound();
        }

        var productInCategory = await _repoManager.Product.GetProductById(categoryId, productId, false);

        if (productInCategory == null) return NotFound();

        var _product = _mapper.Map<ReadProductDto>(productInCategory);
        return Ok(_product);
    }

    [HttpPost(Name = "CreateProductForCategory")]
    public async Task<ActionResult> CreateProductForCategory(string categoryId, CreateProductDto product)
    {
        if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
        {
            return NotFound();
        }

        var productEntity = _mapper.Map<Product>(product);
        _repoManager.Product.CreateProduct(productEntity);
        await _repoManager.SaveAsync();

        var productToReturn = _mapper.Map<ReadProductDto>(productEntity);
        return CreatedAtRoute("GetSingleProductInCategory", new { categoryId, productId = productToReturn.Id }, productToReturn);
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProductFromCategory(string categoryId, string productId)
    {
        if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
        {
            return NotFound();
        }

        var productInCategoryFromRepo = await _repoManager.Product.GetProductById(categoryId, productId, false);

        if (productInCategoryFromRepo == null) return NotFound();

        _repoManager.Product.DeleteProduct(productInCategoryFromRepo);
        await _repoManager.SaveAsync();

        return NoContent();
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult> UpdateProductInACategory(string categoryId, string productId, UpdateProductDto product)
    {
        if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
        {
            return NotFound();
        }

        var productInCategoryFromRepo = await _repoManager.Product.GetProductById(categoryId, productId, false);

        if (productInCategoryFromRepo == null) return NotFound();

        _mapper.Map(product, productInCategoryFromRepo);
        productInCategoryFromRepo.UpdatedAt = DateTime.Now;

        _repoManager.Product.UpdateProduct(productInCategoryFromRepo);
        await _repoManager.SaveAsync();
        return NoContent();
    }
}
