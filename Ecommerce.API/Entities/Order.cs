using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class Order : BaseEntity
{
    public decimal OrderTotal { get; set; }

    [ForeignKey("AddressId")]
    public string AddressId { get; set; }
    public Address Address { get; set; }

    [ForeignKey("OrderStatusId")]
    public string OrderStatusId { get; set; }
    public OrderStatus OrderStatus { get; set; }

    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
