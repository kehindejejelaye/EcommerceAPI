using AutoMapper;
using Ecommerce.API.DTOs.VariantOption;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class VariantOptionProfile : Profile
{
    public VariantOptionProfile()
    {
        CreateMap<VariantOption, ReadVariantOptionDto>();
        CreateMap<CreateVariantOptionDto, VariantOption>();
        CreateMap<UpdateVariantOptionDto, VariantOption>();
    }
}
