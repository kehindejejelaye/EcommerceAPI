using AutoMapper;
using Ecommerce.API.DTOs.User;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, ApplicationUser>();
        CreateMap<ApplicationUser, UserDto>();
    }
}
