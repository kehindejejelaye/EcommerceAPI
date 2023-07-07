using AutoMapper;
using Ecommerce.API.DTOs.ShoppingCartItem;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class ShoppingCartItemProfile : Profile
{
    public ShoppingCartItemProfile()
    {
        CreateMap<CreateShoppingCartItemDto, ShoppingCartItem>();
        CreateMap<UpdateShoppingCartItemDto, ShoppingCartItem>();
        CreateMap<ShoppingCartItem, ReadShoppingCartItemDto>();
    }
}
