using AutoMapper;
using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;


namespace RMall_BE.Repositories
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
            return _context.Movies.FirstOrDefault(x => x.id == id);
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
            return _context.Movies.Any(f => f.id == id);
        }
    }
}
