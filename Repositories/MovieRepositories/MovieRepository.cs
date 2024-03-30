using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Genres;
using RMall_BE.Models.Movies.Languages;

namespace RMall_BE.Repositories.MovieRepositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly RMallContext _context;
        private readonly IMapper _mapper;


        public MovieRepository(RMallContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ICollection<Movie> GetAllMovie()
        {
            var movies = _context.Movies.ToList();
            return movies;
        }


        public Movie GetMovieById(int id)
        {
            return _context.Movies.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Movie> GetMovieByGenreId(int genreId)
        {
            var genreMovies = _context.MovieGenres.Include(m => m.Movie).Where(mg => mg.Genre.Id == genreId).ToList();
            var movieList = new List<Movie>();
            foreach(var movieGenre in genreMovies)
            {
                Movie movie = _context.Movies.FirstOrDefault(m => m.Id == movieGenre.Movie_Id);
                movieList.Add(movie);
            }
            return movieList;
        }
        public ICollection<Movie> GetMovieByLanguageId(int languageId)
        {
            var languageMovies = _context.MovieLanguages.Include(m => m.Movie).Where(lm => lm.Language.Id == languageId).ToList();
            var movieList = new List<Movie>();
            foreach(var  languageMovie in languageMovies)
            {
                Movie movie = _context.Movies.FirstOrDefault(m => m.Id == languageMovie.Movie_id);
                movieList.Add(movie);
            }    
            return movieList;
        }
        public bool CreateMovie(Movie movie)
        {
            _context.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            return Save();
        }


        public bool UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            return Save();
        }

        public bool CreateMovieGenre(MovieGenre movieGenre)
        {
            _context.MovieGenres.Add(movieGenre);
            return Save();
        }
        public bool CreateMovieLanguage(MovieLanguage movieLanguage)
        {
            _context.MovieLanguages.Add(movieLanguage);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool MovieExist(int id)
        {
            return _context.Movies.Any(f => f.Id == id);
        }

        
    }
}
