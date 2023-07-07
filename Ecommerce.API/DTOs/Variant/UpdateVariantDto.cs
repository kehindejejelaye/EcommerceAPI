namespace Ecommerce.API.DTOs.Variant;

public class UpdateVariantDto
{
    public string Name { get; set; }
    public string CategoryId { get; set; }
    private DateTime UpdatedAt { get;  set; } = DateTime.Now;
}
