using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;

namespace RMall_BE.Repositories.MovieRepositories
{
    public class GalleryMovieRepository : IGalleryMovieRepository
    {
        private readonly RMallContext _context;

        public GalleryMovieRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateGalleryMovie(GalleryMovie galleryMovie)
        {
            _context.Add(galleryMovie);
            return Save();
        }
        public bool UpdateGalleryMovie(GalleryMovie galleryMovie)
        {
            _context.Update(galleryMovie);
            return Save();
        }
        public bool DeleteGalleryMovie(GalleryMovie galleryMovie)
        {
            _context.Remove(galleryMovie);
            return Save();
        }

        public bool GalleryMovieExist(int id)
        {
            return _context.GalleryMovies.Any(gm => gm.Id == id);
        }

        public ICollection<GalleryMovie> GetAllGalleryMovie()
        {
            var galleryMovies = _context.GalleryMovies.ToList();
            return galleryMovies; 
        }

        public GalleryMovie GetGalleryMovieById(int id)
        {
            return _context.GalleryMovies.FirstOrDefault(gm => gm.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
