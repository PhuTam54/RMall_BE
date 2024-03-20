using RMall_BE.Models;

namespace RMall_BE.Interfaces
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
