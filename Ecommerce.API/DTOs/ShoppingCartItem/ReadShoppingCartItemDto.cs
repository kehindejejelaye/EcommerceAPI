namespace Ecommerce.API.DTOs.ShoppingCartItem;

public class ReadShoppingCartItemDto
{
    public string Id { get; set; }
    public int Quantity { get; set; } = 1;
    public string ProductItemId { get; set; }
    public string UserId { get; set; }
}
