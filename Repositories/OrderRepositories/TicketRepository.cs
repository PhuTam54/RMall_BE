using RMall_BE.Data;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Orders;

namespace RMall_BE.Repositories.OrderRepositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly RMallContext _context;

        public TicketRepository(RMallContext context)
        {
            _context = context;
        }

        public bool CreateTicket(Ticket ticket)
        {
            _context.Add(ticket);
            return Save();
        }
        public bool UpdateTicket(Ticket ticket)
        {
            _context.Update(ticket);
            return Save();
        }
        public bool DeleteTicket(Ticket ticket)
        {
            _context.Remove(ticket);
            return Save();
        }

        public ICollection<Ticket> GetAllTicket()
        {
            var tickets = _context.Tickets.ToList();
            return tickets;
        }

        public Ticket GetTicketById(int id)
        {
            return _context.Tickets.FirstOrDefault(t => t.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TicketExist(int id)
        {
            return _context.Tickets.Any(t => t.Id == id);
        }


    }
}
