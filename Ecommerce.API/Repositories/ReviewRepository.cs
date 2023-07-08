using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(EcommerceContext ecommerceContext) : base(ecommerceContext)
    {
    }

    public async Task<Review?> GetUserReviewForProudctItem(string userId, string productItemId, bool trackChanges)
    {
        return await FindByCondition(r => r.UserId == userId && r.ProductItemId == productItemId, trackChanges)
            .Include(r => r.ProductItem)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Review>> GetReviewsForProduct(string productId, bool trackChanges)
    {
        return await FindByCondition(r => r.ProductItemId == productId, trackChanges)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingForProduct(string productItemId)
    {
        var averageRating = await FindByCondition(r => r.ProductItemId == productItemId, trackChanges: false)
            .AverageAsync(r => r.RatingValue);

        return Math.Round(averageRating, 2);
    }


    public void CreateReview(Review review) => Create(review);

    public void UpdateReview(Review review) => Update(review);

    public void DeleteReview(Review review) => Delete(review);
}
