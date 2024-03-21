namespace RMall_BE.Models
{
    public class OrderFood
    {
        public int id { get; set; } 
        public int order_id { get; set; }
        public int food_id { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public string note { get; set; }
        public ICollection<Food> food { get; set; }

    }
}
