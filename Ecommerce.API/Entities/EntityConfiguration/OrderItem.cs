using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities.EntityConfiguration;

public class OrderItem : BaseEntity
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("ProductItemId")]
    public string ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; }

    [ForeignKey("OrderId")]
    public string OrderId { get; set; }
    public Order Order { get; set; }
}
