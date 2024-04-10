using AutoMapper;
using RMall_BE.Data;
using RMall_BE.Models.Movies;
using RMall_BE.Interfaces.MovieInterfaces;

namespace RMall_BE.Repositories.MovieRepositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RMallContext _context;
        private readonly IMapper _mapper;


        public ReviewRepository(RMallContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ICollection<Review> GetAllReview()
        {
            var reviews = _context.Reviews.ToList();
            return reviews;
        }


        public Review GetReviewById(int id)
        {
            return _context.Reviews.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Review> GetReviewsbyMovieId(int movieId)
        {
            //var reviewMovies = _context.MovieReviews
            //    .Include(g => g.Review)
            //    .Include(m => m.Movie)
            //    .Where(mg => mg.Movie.Id == movieId).ToList();
            var reviews = new List<Review>();
            //foreach (var reviewMovie in reviewMovies)
            //{
            //    var review = _context.Reviews.FirstOrDefault(g => g.Id == reviewMovie.Review_Id);
            //    reviews.Add(review);
            //}

            return reviews;

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


        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool ReviewExist(int id)
        {
            return _context.Reviews.Any(f => f.Id == id);
        }
    }
}
