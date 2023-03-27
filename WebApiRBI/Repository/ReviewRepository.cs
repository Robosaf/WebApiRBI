using WebApiRBI.Data;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(rev => rev.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsByRestaurantId(int restaurantId)
        {
            return _context.Reviews
                .Where(rev => rev.Restaurant.Id == restaurantId)
                .OrderBy(rev => rev.Id).ToList();
        }

        public Review GetReviewByReviewer(string firstname, string lastname)
        {
            return _context.Reviews.Where(rev =>
            rev.Reviewer.FirstName == firstname && rev.Reviewer.LastName == lastname)
                .FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(rev => rev.Id).ToList();
        }

        public bool ReviewExist(int reviewId)
        {
            return _context.Reviews.Any(rev => rev.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool DeleteReviews(ICollection<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(rev => rev.Reviewer.Id == reviewerId)
                .OrderBy(rev => rev.Id).ToList();
        }
    }
}
