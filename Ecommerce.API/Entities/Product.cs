using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class Product : BaseEntity 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    public string CategoryId { get; set; }

    public List<ProductItem> ProductItems { get; set; }
}
