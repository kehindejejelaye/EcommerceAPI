using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.VariantOption;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/categories/{categoryId}/variants/{variantId}/variantoptions")]
    [ApiController]
    public class VariantOptionsController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public VariantOptionsController(IRepositoryManager repoManager, IMapper mapper)
        {
            _repoManager = repoManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVariantOptionsInVariant(string categoryId, string variantId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Variant.VariantExistsAsync(categoryId, variantId, false))
            {
                return NotFound();
            }

            var variantOptions = await _repoManager.VariantOption.GetAllVariantOptionsInVariant(variantId, false);
            var _variantOptions = _mapper.Map<IEnumerable<ReadVariantOptionDto>>(variantOptions);
            return Ok(_variantOptions);
        }

        [HttpGet("{variantOptionId}", Name = "GetSingleVariantOptionInVariant")]
        public async Task<ActionResult> GetSingleVariantOptionInVariant(string categoryId, string variantId, string variantOptionId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Variant.VariantExistsAsync(categoryId, variantId, false))
            {
                return NotFound();
            }

            var variantOptionInVariant = await _repoManager.VariantOption.GetVariantOptionById(variantId, variantOptionId, false);

            if (variantOptionInVariant == null)
                return NotFound();

            var _variantOption = _mapper.Map<ReadVariantOptionDto>(variantOptionInVariant);
            return Ok(_variantOption);
        }

        [HttpPost(Name = "CreateVariantOptionInVariant")]
        public async Task<ActionResult> CreateVariantOptionInVariant(string categoryId, string variantId, CreateVariantOptionDto variantOption)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Variant.VariantExistsAsync(categoryId, variantId, false))
            {
                return NotFound();
            }

            var variantOptionEntity = _mapper.Map<VariantOption>(variantOption);
            variantOptionEntity.VariantId = variantId;
            _repoManager.VariantOption.CreateVariantOption(variantOptionEntity);
            await _repoManager.SaveAsync();

            var variantOptionToReturn = _mapper.Map<ReadVariantOptionDto>(variantOptionEntity);
            return CreatedAtRoute("GetSingleVariantOptionInVariant", new { categoryId, variantId, variantOptionId = variantOptionToReturn.Id }, variantOptionToReturn);
        }

        [HttpDelete("{variantOptionId}")]
        public async Task<ActionResult> DeleteVariantOptionFromVariant(string categoryId, string variantId, string variantOptionId)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Variant.VariantExistsAsync(categoryId, variantId, false))
            {
                return NotFound();
            }

            var variantOptionInVariantFromRepo = await _repoManager.VariantOption.GetVariantOptionById(variantId, variantOptionId, false);

            if (variantOptionInVariantFromRepo == null)
                return NotFound();

            _repoManager.VariantOption.DeleteVariantOption(variantOptionInVariantFromRepo);
            await _repoManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{variantOptionId}")]
        public async Task<ActionResult> UpdateVariantOptionInVariant(string categoryId, string variantId, string variantOptionId, UpdateVariantOptionDto variantOption)
        {
            if (!await _repoManager.Category.CategoryExistsAsync(categoryId, false))
            {
                return NotFound();
            }

            if (!await _repoManager.Variant.VariantExistsAsync(categoryId, variantId, false))
            {
                return NotFound();
            }

            var variantOptionInVariantFromRepo = await _repoManager.VariantOption.GetVariantOptionById(variantId, variantOptionId, false);

            if (variantOptionInVariantFromRepo == null)
                return NotFound();

            _mapper.Map(variantOption, variantOptionInVariantFromRepo);
            variantOptionInVariantFromRepo.UpdatedAt = DateTime.Now;

            _repoManager.VariantOption.UpdateVariantOption(variantOptionInVariantFromRepo);
            await _repoManager.SaveAsync();

            return NoContent();
        }
    }
}
