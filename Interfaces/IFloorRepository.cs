using RMall_BE.Models;

namespace RMall_BE.Interfaces
{
    public interface IFloorRepository
    {
        ICollection<Floor> GetAllFloor();
        Floor GetFloorById(int id);
        bool CreateFloor(Floor floor);
        bool UpdateFloor(Floor floor);
        bool DeleteFloor(Floor floor);
        bool FloorExist(int id);
        bool Save();
    }
}
