using RMall_BE.Models.Shops;

namespace RMall_BE.Interfaces.ShopInterfaces
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
