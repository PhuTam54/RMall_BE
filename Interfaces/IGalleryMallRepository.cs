using RMall_BE.Models;

namespace RMall_BE.Interfaces.MallInterfaces
{
    public interface IGalleryMallRepository
    {
        ICollection<GalleryMall> GetAllGalleryMall();
        GalleryMall GetGalleryMallById(int id);
        bool CreateGalleryMall(GalleryMall galleryMall);
        bool UpdateGalleryMall(GalleryMall galleryMall);
        bool DeleteGalleryMall(GalleryMall galleryMall);
        bool GalleryMallExist(int id);
        bool Save();
    }
}
