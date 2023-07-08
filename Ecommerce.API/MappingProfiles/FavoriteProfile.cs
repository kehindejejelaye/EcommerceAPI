using AutoMapper;
using Ecommerce.API.DTOs.Favorite;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class FavoriteProfile : Profile
{
    public FavoriteProfile()
    {
        CreateMap<Favorite, ReadFavoriteDto>();
        CreateMap<CreateFavoriteDto, Favorite>();
        CreateMap<UpdateFavoriteDto, Favorite>();
    }
}
