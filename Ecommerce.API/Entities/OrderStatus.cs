namespace Ecommerce.API.Entities.EntityConfiguration;

public class OrderStatus : BaseEntity
{
    public string Status { get; set; }

    public ICollection<Order> Orders { get; set; }
}
