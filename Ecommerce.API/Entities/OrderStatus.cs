namespace Ecommerce.API.Entities;

public class OrderStatus : BaseEntity
{
    public string Status { get; set; }

    public ICollection<Order> Orders { get; set; }
}
