namespace Ecommerce.API.DTOs.Address;

public class UpdateAddressDto
{
    public string? AddressLine { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public bool? IsPreferred { get; set; }
    public string? CountryId { get; set; }
    public string? UserId { get; set; }
}
