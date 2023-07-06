using AutoMapper;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Category, ReadCategoryDto>();
    }
}
