using RMall_BE.Controllers;
using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models.User;

namespace RMall_BE.Repositories.UserRepositories
{
    public class CustomerRepository : IUserRepository<Customer>
    {
        private readonly RMallContext _context;

        public CustomerRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateUser(Customer user)
        {
            _context.Customers.Add(user);
            return Save();
        }
        public bool UpdateUser(Customer user)
        {
            _context.Customers.Update(user);
            return Save();
        }

        public bool DeleteUser(Customer user)
        {
            _context.Customers.Remove(user);
            return Save();
        }

        public ICollection<Customer> GetAllUser()
        {
            var users = _context.Customers.ToList();
            return users;
        }

        public Customer GetUserById(int id)
        {
            var user = _context.Customers.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExist(int id)
        {
            return _context.Customers.Any(u => u.Id == id);
        }
    }
}
