using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies.Genres;

namespace RMall_BE.Repositories.MovieRepositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly RMallContext _context;
        private readonly IMapper _mapper;


        public GenreRepository(RMallContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ICollection<Genre> GetAllGenre()
        {
            var genres = _context.Genres.ToList();
            return genres;
        }


        public Genre GetGenreById(int id)
        {
            return _context.Genres.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Genre> GetGenresbyMovieId(int movieId)
        {
            var genreMovies = _context.MovieGenres
                .Include(g => g.Genre)
                .Include(m => m.Movie)
                .Where(mg => mg.Movie.Id == movieId).ToList();
            var genres = new List<Genre>();
            foreach (var genreMovie in genreMovies)
            {
                var genre = _context.Genres.FirstOrDefault(g => g.Id == genreMovie.Genre_Id);
                genres.Add(genre);
            }

            return genres;

        }

        public bool CreateGenre(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            return Save();
        }


        public bool UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool GenreExist(int id)
        {
            return _context.Genres.Any(f => f.Id == id);
        }

        
    }
}
