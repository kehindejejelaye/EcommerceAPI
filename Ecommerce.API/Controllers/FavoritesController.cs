using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Favorite;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/users/{userId}/favorites")]
public class FavoritesController : ControllerBase
{
    private readonly IRepositoryManager _repoManager;
    private readonly IMapper _mapper;
    private UserManager<ApplicationUser> _userManager;

    public FavoritesController(IRepositoryManager repoManager, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _repoManager = repoManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("{favoriteId}", Name = "GetFavoriteForUserById")]
    public async Task<ActionResult> GetFavoriteForUserById(string userId, string favoriteId)
    {
        var favorite = await _repoManager.Favorite.GetFavoriteForUserById(userId, favoriteId, false);

        if (favorite == null)
        {
            return NotFound();
        }

        var favoriteDto = _mapper.Map<ReadFavoriteDto>(favorite);
        return Ok(favoriteDto);
    }

    [HttpGet]
    public async Task<ActionResult> GetFavoritesForUserByUserId(string userId)
    {
        var favorites = await _repoManager.Favorite.GetFavoritesForUserByUserId(userId, false);
        var favoriteDtos = _mapper.Map<IEnumerable<ReadFavoriteDto>>(favorites);
        return Ok(favoriteDtos);
    }

    [HttpPost]
    public async Task<ActionResult> AddFavorite(string userId, [FromBody] CreateFavoriteDto favoriteDto)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var favoriteEntity = _mapper.Map<Favorite>(favoriteDto);
        favoriteEntity.UserId = userId;

        _repoManager.Favorite.AddFavorite(favoriteEntity);
        await _repoManager.SaveAsync();

        var createdFavoriteDto = _mapper.Map<ReadFavoriteDto>(favoriteEntity);
        return CreatedAtRoute("GetFavoriteForUserById", new { userId, favoriteId = createdFavoriteDto.Id }, createdFavoriteDto);
    }

    [HttpPut("{favoriteId}")]
    public async Task<IActionResult> UpdateFavorite(string userId, string favoriteId, UpdateFavoriteDto favoriteDto)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var favoriteEntity = await _repoManager.Favorite.GetFavoriteForUserById(userId, favoriteId, true);

        if (favoriteEntity == null)
        {
            return NotFound();
        }

        _mapper.Map(favoriteDto, favoriteEntity);
        favoriteEntity.UpdatedAt = DateTime.Now;

        _repoManager.Favorite.UpdateFavorite(favoriteEntity);
        await _repoManager.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{favoriteId}")]
    public async Task<IActionResult> DeleteFavorite(string userId, string favoriteId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var favoriteEntity = await _repoManager.Favorite.GetFavoriteForUserById(userId, favoriteId, true);

        if (favoriteEntity == null)
        {
            return NotFound();
        }

        _repoManager.Favorite.DeleteFavorite(favoriteEntity);
        await _repoManager.SaveAsync();

        return NoContent();
    }
}

