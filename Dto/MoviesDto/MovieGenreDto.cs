namespace RMall_BE.Dto.MoviesDto
{
    public class MovieGenreDto
    {
        public int Id { get; set; }
        public int Movie_Id { get; set; }
        public int Genre_Id { get; set; }
        public GenreDto Genre { get; set; }
    }
}
