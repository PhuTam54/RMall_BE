using RMall_BE.Models.User;

namespace RMall_BE.Models.Movies
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public int User_Id { get; set; }
        public int Movie_Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public DateTime? Deleted_At { get; set; }
        public Customer User { get; set; }
        public Movie Movie { get; set; }

    }
}
