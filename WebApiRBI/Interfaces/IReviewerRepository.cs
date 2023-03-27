using WebApiRBI.Models;

namespace WebApiRBI.Interfaces
{
    public interface IReviewerRepository
    {
        Reviewer GetReviewer(int reviewerId);
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(string firstname, string lastname);
        bool ReviewerExist(int reviewerId);
        bool ReviewerExist(string firstname, string lastname);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
