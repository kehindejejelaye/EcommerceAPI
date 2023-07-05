namespace Ecommerce.API.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
    public List<Variant> Variants { get; set; }
}
