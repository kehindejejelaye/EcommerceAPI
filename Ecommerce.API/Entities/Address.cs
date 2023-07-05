using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class Address : BaseEntity
{
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    [ForeignKey("CountryId")]
    public Country Country { get; set; }
    public string CountryId { get; set; }
}
