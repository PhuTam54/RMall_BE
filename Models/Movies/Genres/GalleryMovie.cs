namespace RMall_BE.Models.Movies.Genres
{
    public class GalleryMovie
    {
        public int Id { get; set; }
        public string Image_Path { get; set; }
        public int Movie_Id { get; set; }
        public Movie Movie { get; set; }
    }
}
