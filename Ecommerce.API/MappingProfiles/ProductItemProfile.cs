using AutoMapper;
using Ecommerce.API.DTOs.ProductItem;
using Ecommerce.API.Entities;
using Ecommerce.API.Repositories;

namespace Ecommerce.API.MappingProfiles;

public class ProductItemProfile : Profile
{
	public ProductItemProfile()
	{
		CreateMap<ProductItem, ReadProductItemDto>();
		CreateMap<CreateProductItemDto, ProductItem>();
		CreateMap<UpdateProductItemDto, ProductItem>();
	}
}
