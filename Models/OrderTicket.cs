namespace RMall_BE.Models
{
    public class OrderTicket
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int movie_id { get; set;}
        public int seat_id { get; set;}
        public decimal price { get; set;}
        public ICollection<Seat> seats { get; set; }
    }
}
