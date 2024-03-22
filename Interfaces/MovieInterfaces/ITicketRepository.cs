using RMall_BE.Models;
using RMall_BE.Models.Movies;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface ITicketRepository
    {
        ICollection<Ticket> GetAllTicket();
        Ticket GetTicketById(int id);
        bool CreateTicket(Ticket ticket);
        bool UpdateTicket(Ticket ticket);
        bool DeleteTicket(Ticket ticket);
        bool TicketExist(int id);
        bool Save();
    }
}
