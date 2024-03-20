using Microsoft.EntityFrameworkCore;
using RMall_BE.Models;

namespace RMall_BE.Data
{
    public class RMallContext : DbContext
    {
        public RMallContext(DbContextOptions<RMallContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }


    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

}
