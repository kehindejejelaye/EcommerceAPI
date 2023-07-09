using Ecommerce.API.Entities;
namespace Ecommerce.API.DTOs.Review;

public class ReadReviewDto
{
    public string Id { get; set; }
    public string Comment { get; set; }
    public int RatingValue { get; set; }
    public ProductItem ProductItem { get; set; }
    public string UserId { get; set; }
}
