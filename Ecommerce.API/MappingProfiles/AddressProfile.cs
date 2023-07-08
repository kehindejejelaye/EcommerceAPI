using AutoMapper;
using Ecommerce.API.DTOs.Address;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<UpdateAddressDto, Address>();
        CreateMap<Address, ReadAddressDto>();
    }
}
