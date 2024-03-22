using Microsoft.EntityFrameworkCore;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Genres;
using RMall_BE.Models.Movies.Languages;
using RMall_BE.Models.Movies.Seats;
using RMall_BE.Models.Orders;
using RMall_BE.Models.Shops;

namespace RMall_BE.Data
{
    public class RMallContext : DbContext
    {
        public RMallContext(DbContextOptions<RMallContext> options) : base(options) { }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderFood> OrderFoods { get; set; }
        public DbSet<Food> Foods { get; set; }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Show> Shows { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<GalleryMovie> GalleryMovies { get; set; }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        public DbSet<Language> Languages { get; set; }
        public DbSet<MovieLanguage> MovieLanguages { get; set; }

        
        
        
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatPricing> SeatPricings { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<SeatReservation> SeatReservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        }
}

}
