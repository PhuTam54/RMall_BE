using RMall_BE.Models.Movies;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReview();
        Review GetReviewById(int id);
        ICollection<Review> GetReviewsbyMovieId(int movieId);
        bool CreateReview(Review genre);
        bool UpdateReview(Review genre);
        bool DeleteReview(Review genre);
        bool ReviewExist(int id);
        bool Save();
    }
}
