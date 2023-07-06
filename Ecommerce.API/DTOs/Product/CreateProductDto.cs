namespace Ecommerce.API.DTOs.Product;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string CategoryId { get; set; }
}
