namespace RMall_BE.Dto.MoviesDto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Actor { get; set; }
        public string Movie_Image { get; set; }
        public string Cover_Image { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Director { get; set; }
        public int Favorite_Count { get; set; }
        public string Trailer { get; set; }
    }
}
