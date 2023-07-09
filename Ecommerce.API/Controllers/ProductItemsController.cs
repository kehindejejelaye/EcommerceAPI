using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.ProductItems;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/categories/{categoryId}/products/{productId}/productitems")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public ProductItemsController(IRepositoryManager repoManager, IMapper mapper)
        {
            _repoManager = repoManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProductItemsInProduct(string categoryId, string productId, [FromQuery] RequestParameters requestParameters)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Product.ProductExistsAsync(categoryId, productId, false))
            {
                return NotFound();
            }

            var productItems = await _repoManager.ProductItem.GetAllProductItemsInProduct(categoryId, productId, false);
            var _productItems = _mapper.Map<IEnumerable<ReadProductItemDto>>(productItems);
            return Ok(_productItems);
        }

        [HttpGet("{productItemId}", Name = "GetSingleProductItemInProduct")]
        public async Task<ActionResult> GetSingleProductItemInProduct(string categoryId, string productId, string productItemId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Product.ProductExistsAsync(categoryId, productId, false))
            {
                return NotFound();
            }

            var productItemInProduct = await _repoManager.ProductItem.GetProductItemByIdWithCategoryIdAndProductId(categoryId, productId, productItemId, false);

            if (productItemInProduct == null)
                return NotFound();

            var _productItem = _mapper.Map<ReadProductItemDto>(productItemInProduct);
            return Ok(_productItem);
        }

        [HttpPost(Name = "CreateProductItemInProduct")]
        public async Task<ActionResult> CreateProductItemInProduct(string categoryId, string productId, CreateProductItemDto productItem)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Product.ProductExistsAsync(categoryId, productId, false))
            {
                return NotFound();
            }

            var productItemEntity = _mapper.Map<ProductItem>(productItem);
            productItemEntity.ProductId = productId;
            _repoManager.ProductItem.CreateProductItem(productItemEntity);
            await _repoManager.SaveAsync();

            var productItemToReturn = _mapper.Map<ReadProductItemDto>(productItemEntity);
            return CreatedAtRoute("GetSingleProductItemInProduct", new { categoryId, productId, productItemId = productItemToReturn.Id }, productItemToReturn);
        }

        [HttpDelete("{productItemId}")]
        public async Task<ActionResult> DeleteProductItemFromProduct(string categoryId, string productId, string productItemId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Product.ProductExistsAsync(categoryId, productId, false))
            {
                return NotFound();
            }

            var productItemInProductFromRepo = await _repoManager.ProductItem.GetProductItemByIdWithCategoryIdAndProductId(categoryId, productId, productItemId, false);

            if (productItemInProductFromRepo == null)
                return NotFound();

            _repoManager.ProductItem.DeleteProductItem(productItemInProductFromRepo);
            await _repoManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{productItemId}")]
        public async Task<ActionResult> UpdateProductItemInProduct(string categoryId, string productId, string productItemId, UpdateProductItemDto productItem)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Product.ProductExistsAsync(categoryId, productId, false))
            {
                return NotFound();
            }

            var productItemInProductFromRepo = await _repoManager.ProductItem.GetProductItemByIdWithCategoryIdAndProductId(categoryId, productId, productItemId, false);

            if (productItemInProductFromRepo == null)
                return NotFound();

            _mapper.Map(productItem, productItemInProductFromRepo);
            productItemInProductFromRepo.UpdatedAt = DateTime.Now;

            _repoManager.ProductItem.UpdateProductItem(productItemInProductFromRepo);
            await _repoManager.SaveAsync();

            return NoContent();
        }
    }
}
