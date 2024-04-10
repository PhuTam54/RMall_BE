
namespace RMall_BE.Dto.MoviesDto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public int User_Id { get; set; }
        public int Movie_Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Deleted_At { get; set; }
    }
}
