using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class Variant : BaseEntity
{
    public string Name { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    public string CategoryId { get; set; }
    public List<VariantOption> VariantOptions { get; set; }
}
