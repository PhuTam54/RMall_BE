using RMall_BE.Models;

namespace RMall_BE.Interfaces
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
