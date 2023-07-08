using Ecommerce.API.Entities;

namespace Ecommerce.API.DTOs.Address;

public class ReadAddressDto
{
    public string Id { get; set; }
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public bool IsPreferred { get; set; }
    public Country Country { get; set; }
    public string UserId { get; set; }
}
