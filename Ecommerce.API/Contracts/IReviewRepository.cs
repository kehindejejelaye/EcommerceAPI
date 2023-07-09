using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface IReviewRepository
{
    void CreateReview(Review review);
    void DeleteReview(Review review);
    Task<Review?> GetUserReviewForProudctItem(string userId, string productItemId, bool trackChanges);
    Task<double> GetAverageRatingForProduct(string productItemId);
    Task<IEnumerable<Review>> GetReviewsForProduct(string productItemId, bool trackChanges);
    Task<IEnumerable<Review>> GetReviewsMadeByAParticularUser(string userId, bool trackChanges);
    void UpdateReview(Review review);
}