using AutoMapper;
using Ecommerce.API.DTOs.WishList;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class WishListProfile : Profile
{
    public WishListProfile()
    {
        CreateMap<WishList, ReadWishListDto>();
        CreateMap<CreateWishListDto, WishList>();
        CreateMap<UpdateWishListDto, WishList>();
    }
}
