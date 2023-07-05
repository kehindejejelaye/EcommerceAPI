using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class ProductItem : BaseEntity
{
    public string SKU { get; set; }
    public string QuantityInStock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    public string ProductId { get; set; }
    public ICollection<ProductItemVariantOption> VariantOptions { get; set; }
}
