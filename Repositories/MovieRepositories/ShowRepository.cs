﻿using RMall_BE.Data;
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
            var shows = _context.Shows.ToList();
            return shows;
        }

        public Show GetShowById(int id)
        {
            return _context.Shows.FirstOrDefault(s => s.Id == id);
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
