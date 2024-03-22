using AutoMapper;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies;

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
