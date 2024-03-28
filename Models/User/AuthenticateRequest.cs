using System.ComponentModel.DataAnnotations;

namespace RMall_BE.Models.User
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
