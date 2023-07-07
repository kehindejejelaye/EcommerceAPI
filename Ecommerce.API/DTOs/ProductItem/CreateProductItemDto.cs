namespace Ecommerce.API.DTOs.ProductItem;

public class CreateProductItemDto
{
    public string SKU { get; set; }
    public string QuantityInStock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductId { get; set; }
}
