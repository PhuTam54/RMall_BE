using RMall_BE.Models;

namespace RMall_BE.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetAllMovie();
        Movie GetMovieById(int id);
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool MovieExist(int id);
        bool Save();
    }
}
