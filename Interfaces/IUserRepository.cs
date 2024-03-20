using RMall_BE.Models;

namespace RMall_BE.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUser();
        User GetUserById(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool UserExist(int id);
        bool Save();
    }
}
