namespace RMall_BE.Models
{
    public class Feedback
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        

    }
}
