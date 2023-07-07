using AutoMapper;
using Ecommerce.API.DTOs.Variant;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class VariantProfile : Profile
{
    public VariantProfile()
    {
        CreateMap<Variant, ReadVariantDto>();
        CreateMap<CreateVaraintDto, Variant>();
        CreateMap<UpdateVariantDto, Variant>();
    }
}
