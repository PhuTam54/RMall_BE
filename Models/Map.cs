namespace RMall_BE.Models
{
    public class Map
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public Shop Shop { get; set; }
    }
}
