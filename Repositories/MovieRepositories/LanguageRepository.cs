using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies.Languages;

namespace RMall_BE.Repositories.MovieRepositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly RMallContext _context;

        public LanguageRepository(RMallContext context)
        {
            _context = context;
        }

        public bool CreateLanguage(Language language)
        {
            _context.Add(language);
            return Save();
        }
        public bool UpdateLanguage(Language language)
        {
            _context.Update(language);
            return Save();
        }
        public bool DeleteLanguage(Language language)
        {
            _context.Remove(language);
            return Save();
        }

        public ICollection<Language> GetAllLanguage()
        {
            var languages = _context.Languages.ToList();
            return languages;
        }

        public Language GetLanguageById(int id)
        {
            return _context.Languages.FirstOrDefault(l => l.Id == id);
        }

        public bool LanguageExist(int id)
        {
            return _context.Languages.Any(l => l.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
