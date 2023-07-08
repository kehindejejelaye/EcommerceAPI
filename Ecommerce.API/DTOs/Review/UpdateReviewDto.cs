namespace Ecommerce.API.DTOs.Review;

public class UpdateReviewDto
{
    public string Comment { get; set; }
    public int RatingValue { get; set; }
    public string ProductItemId { get; set; }
    public string UserId { get; set; }
}
