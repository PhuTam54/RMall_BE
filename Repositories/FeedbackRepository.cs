using AutoMapper;
using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly RMallContext _context;
        private readonly IMapper _mapper;


        public FeedbackRepository(RMallContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<Feedback> GetAllFeedback()
        {
            var feedbacks = _context.Feedbacks.ToList();
            return feedbacks;
        }
        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(x => x.Id == id);
        }
        public bool CreateFeedback(Feedback feedback)
        {
            _context.Add(feedback);
            return Save();
        }

        public bool DeleteFeedback(Feedback feedback)
        {
            _context.Remove(feedback);
            return Save();
        }


        public bool UpdateFeedback(Feedback feedback)
        {
            _context.Update(feedback);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool FeedbackExist(int id)
        {
            return _context.Feedbacks.Any(f => f.Id == id);
        }

    }
}
