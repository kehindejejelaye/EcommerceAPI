using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IRepositoryManager _repoManager;
    private readonly IMapper _mapper;

    public CategoriesController(IRepositoryManager repoManager, IMapper mapper)
    {
        _repoManager = repoManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetCategories([FromQuery] RequestParameters requestParameters)
    {
        var categories = await _repoManager.Category.GetAllCategories(requestParameters, false);
        var _categories = _mapper.Map<IEnumerable<ReadCategoryDto>>(categories);
        return Ok(_categories);
    }

    [HttpGet("{categoryId}", Name = "GetCategory")]
    public async Task<ActionResult> GetCategory(string categoryId)
    {
        var category = await _repoManager.Category.GetCategoryById(categoryId, false);
        var _category = _mapper.Map<ReadCategoryDto>(category);
        return Ok(_category);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory(CreateCategoryDto category)
    {
        var _category = _mapper.Map<Category>(category);
        _repoManager.Category.CreateCategory(_category);
        await _repoManager.SaveAsync();
        var _categoryToReturn = _mapper.Map<ReadCategoryDto>(_category);
        return CreatedAtRoute("GetCategory", new { categoryId = _category.Id }, _categoryToReturn);
    }

    [HttpDelete("{categoryId}")]
    public async Task<ActionResult> DeleteCategory(string categoryId)
    {
        var category = await _repoManager.Category.GetCategoryById(categoryId, true);
        if (category == null) return NotFound();

        _repoManager.Category.DeleteCategory(category);
        await _repoManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{categoryId}")]
    public async Task<ActionResult> UpdateCategory(string categoryId, [FromBody] UpdateCategoryDto category)
    {
        var categoryFromDb = await _repoManager.Category.GetCategoryById(categoryId, true);
        if (categoryFromDb == null) return NotFound();

        _mapper.Map(category, categoryFromDb);
        categoryFromDb.UpdatedAt = DateTime.Now;

        _repoManager.Category.UpdateCategory(categoryFromDb);
        await _repoManager.SaveAsync();
        return NoContent();
    }
}
