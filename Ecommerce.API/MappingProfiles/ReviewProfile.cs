using AutoMapper;
using Ecommerce.API.DTOs.Review;
using Ecommerce.API.Entities;

namespace Ecommerce.API.MappingProfiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReadReviewDto>();
        CreateMap<CreateReviewDto, Review>();
        CreateMap<UpdateReviewDto, Review>();
    }
}
