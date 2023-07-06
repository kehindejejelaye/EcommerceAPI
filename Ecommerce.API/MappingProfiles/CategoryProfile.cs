using AutoMapper;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryDto, CreateCategoryDto>();
        CreateMap<UpdateCategoryDto, CreateCategoryDto>();
        CreateMap<Category, ReadCategoryDto>();
    }
}
