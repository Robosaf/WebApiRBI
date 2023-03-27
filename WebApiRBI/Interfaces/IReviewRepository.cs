using WebApiRBI.Models;

namespace WebApiRBI.Interfaces
{
    public interface IReviewRepository
    {
        Review GetReview(int reviewId);
        ICollection<Review> GetReviews();
        Review GetReviewByReviewer(string firstname, string lastname);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        ICollection<Review> GetReviewsByRestaurantId(int restaurantId);
        bool ReviewExist(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(ICollection<Review> reviews);
        bool Save();
    }
}
