using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Variant;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/categories/{categoryId}/variants")]
    [ApiController]
    public class VariantsController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public VariantsController(IRepositoryManager repoManager, IMapper mapper)
        {
            _repoManager = repoManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVariantsInACategory(string categoryId)
        {
            var variants = await _repoManager.Variant.GetAllVariantsInACategory(categoryId, false);
            var _variants = _mapper.Map<IEnumerable<ReadVariantDto>>(variants);
            return Ok(_variants);
        }

        [HttpGet("{variantId}", Name = "GetSingleVariantInCategory")]
        public async Task<ActionResult> GetSingleVariantInCategory(string categoryId, string variantId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            var variantInCategory = await _repoManager.Variant.GetVariantById(categoryId, variantId, false);

            if (variantInCategory == null) return NotFound();

            var _variant = _mapper.Map<ReadVariantDto>(variantInCategory);
            return Ok(_variant);
        }

        [HttpPost(Name = "CreateVariantForCategory")]
        public async Task<ActionResult> CreateVariantForCategory(string categoryId, CreateVariantDto variant)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            var variantEntity = _mapper.Map<Variant>(variant);
            _repoManager.Variant.CreateVariant(variantEntity);
            await _repoManager.SaveAsync();

            var variantToReturn = _mapper.Map<ReadVariantDto>(variantEntity);
            return CreatedAtRoute("GetSingleVariantInCategory", new { categoryId, variantId = variantToReturn.Id }, variantToReturn);
        }

        [HttpDelete("{variantId}")]
        public async Task<ActionResult> DeleteVariantFromCategory(string categoryId, string variantId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            var variantInCategoryFromRepo = await _repoManager.Variant.GetVariantById(categoryId, variantId, false);

            if (variantInCategoryFromRepo == null) return NotFound();

            _repoManager.Variant.DeleteVariant(variantInCategoryFromRepo);
            await _repoManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{variantId}")]
        public async Task<ActionResult> UpdateVariantInACategory(string categoryId, string variantId, UpdateVariantDto variant)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            var variantInCategoryFromRepo = await _repoManager.Variant.GetVariantById(categoryId, variantId, false);

            if (variantInCategoryFromRepo == null) return NotFound();

            _mapper.Map(variant, variantInCategoryFromRepo);
            variantInCategoryFromRepo.UpdatedAt = DateTime.Now;

            _repoManager.Variant.UpdateVariant(variantInCategoryFromRepo);
            await _repoManager.SaveAsync();
            return NoContent();
        }
    }
}
