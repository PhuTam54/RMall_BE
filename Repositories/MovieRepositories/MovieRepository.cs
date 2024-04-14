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
            var movies = _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                    .ThenInclude(ml => ml.Language)
                .Include(m => m.GalleryMovies)
                .Where(m => m.Shows.Where(s => s.Start_Date.AddDays(7) > DateTime.Now).Any())
                .ToList();
            return movies;
        }

        public ICollection<Movie> GetAllMovieForAdmin()
        {
            var movies = _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                    .ThenInclude(ml => ml.Language)
                .Include(m => m.GalleryMovies)
                .ToList();
            return movies;
        }


        public Movie GetMovieById(int id)
        {
            return _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                    .ThenInclude(ml => ml.Language)
                .Include(m => m.GalleryMovies)
                .FirstOrDefault(x => x.Id == id);
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
        public bool DeleteMovieGenresByMovieId(int id)
        {
            foreach (var movieGenre in _context.MovieGenres.Where(mg => mg.Movie_Id == id))
            {
                _context.MovieGenres.Remove(movieGenre);
            }
            return Save();
        }
        public bool DeleteMovieLanguagesByMovieId(int id)
        {
            foreach (var movieLanguage in _context.MovieLanguages.Where(ml => ml.Movie_id == id))
            {
                _context.MovieLanguages.Remove(movieLanguage);
            }
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

        public double ConvertTimeToDouble(string time)
        {
            double hours = 0;
            double minutes = 0;
            double seconds = 0;

            string[] parts = time.Split(' ');

            foreach (string part in parts)
            {
                if (part.EndsWith("hrs"))
                {
                    double.TryParse(part.Replace("hrs", ""), out hours);
                }
                else if (part.EndsWith("mins"))
                {
                    double.TryParse(part.Replace("mins", ""), out minutes);
                }
                else if (part.EndsWith("ses"))
                {
                    double.TryParse(part.Replace("ses", ""), out seconds);
                }
            }

            double totalMinutes = hours * 60 + minutes + seconds / 60;
            double totalSeconds = totalMinutes * 60; // Convert total minutes to seconds
            return totalSeconds;
        }

    }
}
