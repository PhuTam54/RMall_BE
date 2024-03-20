using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RMallContext _context;

        public RoomRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateRoom(Room room)
        {
            _context.Add(room);
            return Save();
        }
        public bool UpdateRoom(Room room)
        {
            _context.Update(room);
            return Save();
        }
        public bool DeleteRoom(Room room)
        {
            _context.Remove(room);
            return Save();
        }

        public ICollection<Room> GetAllRoom()
        {
            var rooms = _context.Rooms.ToList();
            return rooms;
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.Id == id);
        }
        public Room GetRoomByName(string name)
        {
            return _context.Rooms.FirstOrDefault(r => r.Name == name);
        }
        public bool RoomExist(int id)
        {
            return _context.Rooms.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
