using RMall_BE.Data;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;


namespace RMall_BE.Repositories.ShopRepositories
{
    public class FloorRepository : IFloorRepository
    {
        private readonly RMallContext _context;

        public FloorRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateFloor(Floor floor)
        {
            _context.Add(floor);
            return Save();
        }
        public bool UpdateFloor(Floor floor)
        {
            _context.Update(floor);
            return Save();
        }
        public bool DeleteFloor(Floor floor)
        {
            _context.Remove(floor);
            return Save();
        }

        public ICollection<Floor> GetAllFloor()
        {
            var floors = _context.Floors.ToList();
            return floors;
        }

        public Floor GetFloorById(int id)
        {
            return _context.Floors.FirstOrDefault(f => f.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool FloorExist(int id)
        {
            return _context.Floors.Any(f => f.Id == id);
        }
    }
}
