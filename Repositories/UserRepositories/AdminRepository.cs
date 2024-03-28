using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models.User;

namespace RMall_BE.Repositories
{
    public class AdminRepository : IUserRepository<Admin>
    {
        private readonly RMallContext _context;

        public AdminRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateUser(Admin user)
        {
            _context.Admins.Add(user);
            return Save();
        }
        public bool UpdateUser(Admin user)
        {
            _context.Admins.Update(user);
            return Save();
        }

        public bool DeleteUser(Admin user)
        {
            _context.Admins.Remove(user);
            return Save();
        }

        public ICollection<Admin> GetAllUser()
        {
            var users = _context.Admins.ToList();
            return users;
        }

        public Admin GetUserById(int id)
        {
            var user = _context.Admins.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExist(int id)
        {
            return _context.Admins.Any(u => u.Id == id);
        }
    }
}
