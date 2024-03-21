using AutoMapper;
using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
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
            return _context.Genres.FirstOrDefault(x => x.id == id);
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
            return _context.Genres.Any(f => f.id == id);
        }
    }
}
