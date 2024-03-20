using RMall_BE.Models;

namespace RMall_BE.Interfaces
{
    public interface IFeedbackRepository
    {
        ICollection<Feedback> GetAllFeedback();
        Feedback GetFeedbackById(int id);
        bool CreateFeedback(Feedback feedback);
        bool UpdateFeedback(Feedback feedback);
        bool DeleteFeedback(Feedback feedback);
        bool FeedbackExist(int id);
        bool Save();
    }
}
