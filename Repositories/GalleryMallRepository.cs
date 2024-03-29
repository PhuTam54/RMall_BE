using RMall_BE.Data;
using RMall_BE.Interfaces.MallInterfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories.MallRepositories
{
    public class GalleryMallRepository : IGalleryMallRepository
    {
        private readonly RMallContext _context;

        public GalleryMallRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateGalleryMall(GalleryMall galleryMall)
        {
            _context.Add(galleryMall);
            return Save();
        }
        public bool UpdateGalleryMall(GalleryMall galleryMall)
        {
            _context.Update(galleryMall);
            return Save();
        }
        public bool DeleteGalleryMall(GalleryMall galleryMall)
        {
            _context.Remove(galleryMall);
            return Save();
        }

        public bool GalleryMallExist(int id)
        {
            return _context.GalleryMalls.Any(gm => gm.Id == id);
        }

        public ICollection<GalleryMall> GetAllGalleryMall()
        {
            var galleryMalls = _context.GalleryMalls.ToList();
            return galleryMalls; 
        }

        public GalleryMall GetGalleryMallById(int id)
        {
            return _context.GalleryMalls.FirstOrDefault(gm => gm.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
