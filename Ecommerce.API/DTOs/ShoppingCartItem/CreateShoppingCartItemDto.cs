using Ecommerce.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.DTOs.ShoppingCartItem;

public class CreateShoppingCartItemDto
{
    public int Quantity { get; set; } = 1;
    public string ProductItemId { get; set; }
    public string UserId { get; set; }
}
