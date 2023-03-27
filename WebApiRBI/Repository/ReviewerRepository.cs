using System.Reflection.Metadata.Ecma335;
using WebApiRBI.Data;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault(); 
        }

        public Reviewer GetReviewer(string firstname, string lastname)
        {
            return _context.Reviewers.Where(r => r.FirstName == firstname && r.LastName == lastname).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool ReviewerExist(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool ReviewerExist(string firstname, string lastname)
        {
            return _context.Reviewers.Any(r => r.FirstName == firstname && r.LastName == lastname);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }
    }
}
