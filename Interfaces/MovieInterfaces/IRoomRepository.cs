using RMall_BE.Models.Movies;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface IRoomRepository
    {
        ICollection<Room> GetAllRoom();
        Room GetRoomById(int id);
        Room GetRoomByName(string name);
        bool CreateRoom(Room room);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
        bool RoomExist(int id);
        bool Save();
    }
}
