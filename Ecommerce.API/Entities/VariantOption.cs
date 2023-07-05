using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class VariantOption : BaseEntity
{
    public string Value { get; set; }

    [ForeignKey("VariantId")]
    public Variant Variant { get; set; }
    public string VariantId { get; set; }
    public ICollection<ProductItemVariantOption> ProductItems { get; set; }
}
