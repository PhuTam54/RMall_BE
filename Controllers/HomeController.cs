using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RMall_BE.Models;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Interfaces;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace RMall_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly RMallContext _context;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public HomeController(RMallContext context, IConfiguration config, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with JWT";
        }

        [HttpGet]
        [Route("Details")]
        public string GetDetails()
        {
            return "Details";
        }

        [Authorize]
        [HttpPost]
        public string AddUser(UserDto user)
        {
            return "Add user successfully with user: " + user.Email;
        }

    }
}
