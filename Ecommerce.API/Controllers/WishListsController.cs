using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.WishList;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/wishlists")]
    public class WishListsController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public WishListsController(IRepositoryManager repoManager, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repoManager = repoManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{wishListId}", Name = "GetWishListForUserById")]
        public async Task<ActionResult> GetWishListForUserById(string userId, string wishListId)
        {
            var wishList = await _repoManager.WishList.GetWishListForUserById(userId, wishListId, false);

            if (wishList == null)
            {
                return NotFound();
            }

            var wishListDto = _mapper.Map<ReadWishListDto>(wishList);
            return Ok(wishListDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetWishListsForUserByUserId(string userId)
        {
            var wishLists = await _repoManager.WishList.GetWishListsForUserByUserId(userId, false);
            var wishListDtos = _mapper.Map<IEnumerable<ReadWishListDto>>(wishLists);
            return Ok(wishListDtos);
        }

        [HttpPost]
        public async Task<ActionResult> AddWishList(string userId, [FromBody] CreateWishListDto wishListDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var wishListEntity = _mapper.Map<WishList>(wishListDto);
            wishListEntity.UserId = userId;

            _repoManager.WishList.AddWishList(wishListEntity);
            await _repoManager.SaveAsync();

            var createdWishListDto = _mapper.Map<ReadWishListDto>(wishListEntity);
            return CreatedAtRoute("GetWishListForUserById", new { userId, wishListId = createdWishListDto.Id }, createdWishListDto);
        }

        //[HttpPut("{wishListId}")]
        //public async Task<IActionResult> UpdateWishList(string userId, string wishListId, UpdateWishListDto wishListDto)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var wishListEntity = await _repoManager.WishList.GetWishListForUserById(userId, wishListId, true);

        //    if (wishListEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(wishListDto, wishListEntity);
        //    wishListEntity.UpdatedAt = DateTime.Now;

        //    _repoManager.WishList.UpdateWishList(wishListEntity);
        //    await _repoManager.SaveAsync();

        //    return NoContent();
        //}

        [HttpDelete("{wishListId}")]
        public async Task<IActionResult> DeleteWishList(string userId, string wishListId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var wishListEntity = await _repoManager.WishList.GetWishListForUserById(userId, wishListId, true);

            if (wishListEntity == null)
            {
                return NotFound();
            }

            _repoManager.WishList.DeleteWishList(wishListEntity);
            await _repoManager.SaveAsync();

            return NoContent();
        }
    }
}
