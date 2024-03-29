using RMall_BE.Models.User;

namespace RMall_BE.Models.Movies
{
    public class Favorite
    {
        public int Id { get; set; }
        public int User_Id { get; set; }

        public int Movie_Id { get; set; }
        public DateTime Created_At { get; set; }
        public Customer User { get; set; }
        public Movie Movie { get; set; }

    }
}
