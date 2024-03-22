namespace RMall_BE.Models.Movies.Languages
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieLanguage> MovieLanguages { get; set; }
    }
}
