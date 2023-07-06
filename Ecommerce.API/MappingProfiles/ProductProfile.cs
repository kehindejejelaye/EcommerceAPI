using AutoMapper;
using Ecommerce.API.DTOs.Product;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ReadProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
