using RMall_BE.Models;
using RMall_BE.Models.Movies;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface IShowRepository
    {
        ICollection<Show> GetAllShow();
        Show GetShowById(int id);
        ICollection<Show> GetShowByMovieId(int movieId);
        ICollection<Show> GetShowByRoomId(int roomId);
		ICollection<Show> GetShowing(DateTime timeNow);
		bool CreateShow(Show show);
        bool UpdateShow(Show show);
        bool DeleteShow(Show show);
        bool ShowExist(int id);
        bool Save();
    }
}
