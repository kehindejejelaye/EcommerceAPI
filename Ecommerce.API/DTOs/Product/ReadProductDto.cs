namespace Ecommerce.API.DTOs.Product;

public class ReadProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string CategoryId { get; set; }
}
