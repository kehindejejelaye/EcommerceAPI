namespace Ecommerce.API.DTOs.ShoppingCartItem;

public class UpdateShoppingCartItemDto
{
    public int Quantity { get; set; } = 1;
    public string ProductItemId { get; set; }
    public string UserId { get; set; }
}
