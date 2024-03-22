using RMall_BE.Models.Movies.Languages;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface ILanguageRepository
    {
        ICollection<Language> GetAllLanguage();
        Language GetLanguageById(int id);
        bool CreateLanguage(Language language);
        bool UpdateLanguage(Language language);
        bool DeleteLanguage(Language language);
        bool LanguageExist(int id);
        bool Save();
    }
}
