using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RMall_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly RMallContext _context;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginController(RMallContext context, IConfiguration config, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // JWT Authentication
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            IActionResult response = Unauthorized();
            //if (HttpContext.Session.GetString("UserName") == null)
            {
                var account = _context.Users.Where(x => x.Email.Equals(user.Email)).FirstOrDefault();
                //&& BCrypt.Net.BCrypt.Verify(user.Password, account.Password)
                if (account != null )
                {
                    // Tạo danh sách claim
                    var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes., user.Password),
                    new Claim(ClaimTypes.Email, user.Email),
                    // Thêm các claim khác nếu cần thiết
                };

                    // Tạo JWT token
                    var token = GenerateJwtToken(_config["JwtSettings:SecretKey"],
                        _config["JwtSettings:Issuer"], _config["JwtSettings:Audience"],
                        int.Parse(_config["JwtSettings:ExpirationMinutes"]), claims);

                    // Lưu JWT token vào cookie
                    //Response.Cookies.Append("jwtToken", token, new CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    SameSite = SameSiteMode.Strict,
                    //    Secure = true // Đặt true nếu bạn chỉ muốn gửi cookie qua kết nối HTTPS
                    //});

                    // Trả về JWT token cho người dùng
                    return Ok(new { Token = token });
                }
            }

            return response; // Trả về Unauthorized nếu xác thực không thành công
        }

        private string GenerateJwtToken(string secretKey, string issuer, string audience, int expirationMinutes, IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
