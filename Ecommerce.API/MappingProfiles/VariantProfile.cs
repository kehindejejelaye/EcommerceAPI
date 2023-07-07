using AutoMapper;
using Ecommerce.API.DTOs.Variant;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class VariantProfile : Profile
{
    public VariantProfile()
    {
        CreateMap<Variant, ReadVariantDto>();
        CreateMap<CreateVariantDto, Variant>();
        CreateMap<UpdateVariantDto, Variant>();
    }
}
