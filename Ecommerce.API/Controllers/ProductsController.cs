using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Product;
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
    public async Task<ActionResult> GetAllProductsInACategory(string categoryId)
    {
        var products = await _repoManager.Product.GetAllProductsInACategory(categoryId, false);
        var _products = _mapper.Map<IEnumerable<ReadProductDto>>(products);
        return Ok(_products);
    }
}
