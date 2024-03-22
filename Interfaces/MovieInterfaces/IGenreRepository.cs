using RMall_BE.Models.Movies.Genres;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetAllGenre();
        Genre GetGenreById(int id);
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool GenreExist(int id);
        bool Save();
    }
}
