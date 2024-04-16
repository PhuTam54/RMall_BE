using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;

namespace RMall_BE.Repositories.MovieRepositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly RMallContext _context;

        public ShowRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateShow(Show show)
        {
            _context.Add(show);
            return Save();
        }
        public bool UpdateShow(Show show)
        {
            _context.Update(show);
            return Save();
        }
        public bool DeleteShow(Show show)
        {
            _context.Remove(show);
            return Save();
        }

		public ICollection<Show> GetAllShow()
		{
			var shows = _context.Shows
				.Include(m => m.Movie)
				.Include(r => r.Room)
				.ThenInclude(s => s.Seats)
				.ToList();
			return shows;
		}

		
		public ICollection<Show> GetShowing(DateTime timeNow)
		{
			//var timeNow = DateTime.UtcNow;
			var shows = _context.Shows
				.Include(m => m.Movie)
				.Include(r => r.Room)
				.ThenInclude(s => s.Seats)
				.Include(o => o.Orders)
				.ThenInclude(t => t.Tickets)
				.ToList();
			List<Show> showing = new List<Show>();

			foreach (var show in shows)
			{
				string[] parts = show.Movie.Duration.Split(new string[] { " hrs ", " mins" }, StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length == 2)
				{
					if (int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
					{
						TimeSpan movieDuration = new TimeSpan(hours, minutes, 0);
						DateTime showEndTime = show.Start_Date + movieDuration;

						if (timeNow >= show.Start_Date && timeNow <= showEndTime)
						{
							showing.Add(show);
						}
					}
				}

			}
			showing = showing.OrderByDescending(s => s.GetNumberOfReservedSeats()).ToList();
			return showing;
		}

		public Show GetShowById(int id)
        {
            return _context.Shows.FirstOrDefault(s => s.Id == id);
        }
        public ICollection<Show> GetShowByMovieId(int movieId)
        {
            var shows = _context.Shows.Where(s => s.Movie_Id == movieId
                && s.Start_Date.AddHours(1) > DateTime.UtcNow).ToList();
            return shows;
        }
        public ICollection<Show> GetShowByRoomId(int roomId)
        {
            var shows = _context.Shows.Where(s => s.Room_Id == roomId).ToList();
            return shows;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ShowExist(int id)
        {
            return _context.Shows.Any(s => s.Id == id);
        }

        
    }
}
