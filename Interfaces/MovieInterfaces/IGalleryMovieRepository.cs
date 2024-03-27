using RMall_BE.Models.Movies;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface IGalleryMovieRepository
    {
        ICollection<GalleryMovie> GetAllGalleryMovie();
        GalleryMovie GetGalleryMovieById(int id);
        bool CreateGalleryMovie(GalleryMovie galleryMovie);
        bool UpdateGalleryMovie(GalleryMovie galleryMovie);
        bool DeleteGalleryMovie(GalleryMovie galleryMovie);
        bool GalleryMovieExist(int id);
        bool Save();
    }
}
