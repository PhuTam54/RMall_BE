using RMall_BE.Models.Movies;

namespace RMall_BE.Interfaces.MovieInterfaces
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
