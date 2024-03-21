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
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Language> Languages { get; set; }


<<<<<<< Updated upstream

=======
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<GenreMovie> GenreMovies => Set<GenreMovie>();
        public DbSet<OrderTicket> OrderTickets => Set<OrderTicket>();
        public DbSet<OrderFood> OrderFoods => Set<OrderFood>();      
        public DbSet<Food> Foods => Set<Food>();
        public DbSet<Order> orders => Set<Order>();
     
>>>>>>> Stashed changes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<Genre>()
              .Ignore(g => g.GenreMovies);
            modelBuilder.Entity<Movie>()
                .Ignore(a => a.GenreMovies);
        }
}

}
