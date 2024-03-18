using Microsoft.EntityFrameworkCore;

namespace RMall_BE
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Todo> Todo => Set<Todo>();
    }
}
