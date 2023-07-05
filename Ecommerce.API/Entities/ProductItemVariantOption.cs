using System.Data;

namespace Ecommerce.API.Entities;

public class ProductItemVariantOption
{
    public string ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; }
    public string VariantOptionId { get; set; }
    public VariantOption VariantOption { get; set; }
}
