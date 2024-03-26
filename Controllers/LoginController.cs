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
using RMall_BE.Models;

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

        const int CUSTOMER = 1;
        const int ADMIN = 2;
        const int TENANT = 3;

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
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            IActionResult response = Unauthorized();
            {
                var account = _context.Users.Where(x => x.Email.Equals(loginModel.Email)).FirstOrDefault();
                //&& BCrypt.Net.BCrypt.Verify(loginModel.Password, account.Password)
                if (account != null )
                {
                    var userRole = "Guest";
                    if (account.Role == CUSTOMER)
                    {
                        userRole = "Customer";
                    } else if (account.Role == ADMIN)
                    {
                        userRole = "Admin";
                    } else if (account.Role == TENANT)
                    {
                        userRole = "Tenant";
                    }
                    // Tạo danh sách claim
                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.Role, account.Role), // 1.Guest / 2.Admin / 3.Tenant
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim("userRole", userRole)
                    };
                    // Tạo JWT token
                    var token = GenerateJwtToken(_config["JwtSettings:SecretKey"],
                        _config["JwtSettings:Issuer"], _config["JwtSettings:Audience"],
                        int.Parse(_config["JwtSettings:ExpirationMinutes"]), claims);

                    // Lưu JWT token vào cookie
                    Response.Cookies.Append("jwt", token, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                        Secure = true // Đặt true nếu bạn chỉ muốn gửi cookie qua kết nối HTTPS
                    });

                    // Trả về JWT token cho người dùng
                    return Ok(new { Token = token });
                }
            }

            return response; // Trả về Unauthorized nếu xác thực không thành công
        }

        private string GenerateJwtToken(string secretKey, string issuer, string audience, int expirationMinutes, IEnumerable<Claim> claims)
        {
            //// Tạo danh sách permissions dựa trên vai trò của người dùng
            //var permissions = GetPermissionsForUser(claims);

            //// Thêm claim "Permissions" vào danh sách claims
            //var claimsList = new List<Claim>(claims);
            //claimsList.AddRange(permissions);

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

        private IEnumerable<Claim> GetPermissionsForUser(IEnumerable<Claim> claims)
        {
            // Logic để xác định và trả về danh sách permissions dựa trên vai trò của người dùng (claims)
            // Ví dụ: nếu người dùng có vai trò "Admin", gán cho họ quyền hạn "edit_user", "delete_user", v.v.
            var permissions = new List<Claim>();

            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    if (claim.Value == "Admin")
                    {
                        permissions.Add(new Claim("Permissions", "edit_user"));
                        permissions.Add(new Claim("Permissions", "delete_user"));
                    }
                    // Thêm các logic xử lý khác tương ứng với các vai trò khác
                }
            }

            return permissions;
        }
    }
}
