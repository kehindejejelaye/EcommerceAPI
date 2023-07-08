using Ecommerce.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.DTOs.Address;

public class CreateAddressDto
{
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public bool IsPreferred { get; set; } = false;
    public string CountryId { get; set; }
    public string UserId { get; set; }
}
