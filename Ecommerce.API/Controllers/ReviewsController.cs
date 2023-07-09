using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Review;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/users/{userId}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IRepositoryManager _repoManager;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewsController(IRepositoryManager repoManager, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _repoManager = repoManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("{reviewId}", Name = "GetReviewForUserById")]
    public async Task<ActionResult> GetReviewForUserById(string userId, string reviewId)
    {
        var review = await _repoManager.Review.GetUserReviewForProudctItem(userId, reviewId, false);

        if (review == null)
        {
            return NotFound();
        }

        var reviewDto = _mapper.Map<ReadReviewDto>(review);
        return Ok(reviewDto);
    }

    [HttpGet]
    public async Task<ActionResult> GetReviewsForUserByUserId(string userId)
    {
        var reviews = await _repoManager.Review.GetReviewsMadeByAParticularUser(userId, false);
        var reviewDtos = _mapper.Map<IEnumerable<ReadReviewDto>>(reviews);
        return Ok(reviewDtos);
    }

    [HttpPost]
    public async Task<ActionResult> AddReview(string userId, [FromBody] CreateReviewDto reviewDto)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var reviewEntity = _mapper.Map<Review>(reviewDto);
        reviewEntity.UserId = userId;

        _repoManager.Review.CreateReview(reviewEntity);
        await _repoManager.SaveAsync();

        var createdReviewDto = _mapper.Map<ReadReviewDto>(reviewEntity);
        return CreatedAtRoute("GetReviewForUserById", new { userId, reviewId = createdReviewDto.Id }, createdReviewDto);
    }

    [HttpPut("{reviewId}")]
    public async Task<IActionResult> UpdateReview(string userId, string reviewId, UpdateReviewDto reviewDto)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var reviewEntity = await _repoManager.Review.GetUserReviewForProudctItem(userId, reviewId, true);

        if (reviewEntity == null)
        {
            return NotFound();
        }

        _mapper.Map(reviewDto, reviewEntity);
        reviewEntity.UpdatedAt = DateTime.Now;

        _repoManager.Review.UpdateReview(reviewEntity);
        await _repoManager.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(string userId, string reviewId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var reviewEntity = await _repoManager.Review.GetUserReviewForProudctItem(userId, reviewId, true);

        if (reviewEntity == null)
        {
            return NotFound();
        }

        _repoManager.Review.DeleteReview(reviewEntity);
        await _repoManager.SaveAsync();

        return NoContent();
    }
}
