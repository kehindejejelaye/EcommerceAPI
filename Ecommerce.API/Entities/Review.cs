using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class Review : BaseEntity
{
    public string Comment { get; set; }
    public int RatingValue { get; set; }

    [ForeignKey("ProductItemId")]
    public string ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; }

    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
