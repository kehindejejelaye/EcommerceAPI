using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.ShoppingCartItem;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/users/{userId}/shoppingcart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IRepositoryManager _repoManager;
    private readonly IMapper _mapper;
    private UserManager<ApplicationUser> _userManager;

    public ShoppingCartController(IRepositoryManager repoManager, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _repoManager = repoManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(string userId, CreateShoppingCartItemDto cartItemDto)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        { 
            return NotFound();
        }

        var checkIfItemExists = await _repoManager.ProductItem.DoesProductItemExist(cartItemDto.ProductItemId, false);

        if (!checkIfItemExists) 
        {
            return NotFound();
        }

        var shoppingCartItem = _mapper.Map<ShoppingCartItem>(cartItemDto);
        shoppingCartItem.UserId = userId;

        _repoManager.ShoppingCartItem.AddToCart(shoppingCartItem);
        await _repoManager.SaveAsync();

        //var cartItemToReturn = _mapper.Map<ReadShoppingCartItemDto>(shoppingCartItem);
        //return CreatedAtRoute("GetSingleShoppingCartItem", new { userId, cartItemId = cartItemToReturn.Id }, cartItemToReturn);
        return NoContent();
    }

    [HttpDelete("{productItemId}")]
    public async Task<IActionResult> RemoveFromCart(string userId, string productItemId, [FromBody] int quantity)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var checkIfItemExists = await _repoManager.ProductItem.DoesProductItemExist(productItemId, false);

        if (!checkIfItemExists)
        {
            return NotFound();
        }

        var shoppingCartItem = await _repoManager.ShoppingCartItem.GetShoppingCartItemByProductItemId(userId, productItemId);

        if (shoppingCartItem == null)
        {
            return NotFound();
        }

        var itemToDelete = shoppingCartItem;
        itemToDelete.Quantity = quantity;

        _repoManager.ShoppingCartItem.RemoveFromCart(itemToDelete);
        await _repoManager.SaveAsync();

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetShoppingCartItems(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var cartItems = await _repoManager.ShoppingCartItem.GetShoppingCartItems(userId);
        var cartItemsDto = _mapper.Map<IEnumerable<ReadShoppingCartItemDto>>(cartItems);
        return Ok(cartItemsDto);
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearShoppingCart(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        _repoManager.ShoppingCartItem.ClearShoppingCart(userId);
        await _repoManager.SaveAsync();

        return NoContent();
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetShoppingCartTotal(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var total = _repoManager.ShoppingCartItem.GetShoppingCartTotal(userId);
        return Ok(total);
    }

    [HttpGet("checkout")]
    public async Task<IActionResult> Checkout(string userId)
    {
        var itemsInCart = await _repoManager.ShoppingCartItem.GetShoppingCartItems(userId);

        if (itemsInCart.Count == 0)
        {
            return BadRequest("You cannot checkout an empty cart");
        }


        return Ok();
    }
}
