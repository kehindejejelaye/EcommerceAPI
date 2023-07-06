﻿using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.Entities;
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
    public async Task<ActionResult> GetCategories()
    {
        var categories = await _repoManager.Category.GetAllCategories(false);
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
}
