using RMall_BE.Models.User;

namespace RMall_BE.Interfaces
{
    public interface IUserRepository<T> where T : User
    {
        ICollection<T> GetAllUser();
        T GetUserById(int id);
        bool CreateUser(T user);
        bool UpdateUser(T user);
        bool DeleteUser(T user);
        bool UserExist(int id);
        bool Save();
    }
}
