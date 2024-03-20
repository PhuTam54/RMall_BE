using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RMallContext _context;

        public UserRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }
        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public ICollection<User> GetAllUser()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        

        public bool UserExist(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
