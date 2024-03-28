using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models.User;

namespace RMall_BE.Repositories
{
    public class TenantRepository : IUserRepository<Tenant>
    {
        private readonly RMallContext _context;

        public TenantRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateUser(Tenant user)
        {
            _context.Tenants.Add(user);
            return Save();
        }
        public bool UpdateUser(Tenant user)
        {
            _context.Tenants.Update(user);
            return Save();
        }

        public bool DeleteUser(Tenant user)
        {
            _context.Tenants.Remove(user);
            return Save();
        }

        public ICollection<Tenant> GetAllUser()
        {
            var users = _context.Tenants.ToList();
            return users;
        }

        public Tenant GetUserById(int id)
        {
            var user = _context.Tenants.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExist(int id)
        {
            return _context.Tenants.Any(u => u.Id == id);
        }
    }
}
