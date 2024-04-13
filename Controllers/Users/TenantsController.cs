﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.UsersDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Models.User;
using RMall_BE.Repositories.UserRepositories;

namespace RMall_BE.Controllers.Users
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TenantsController : Controller
    {
        private readonly IUserRepository<Tenant> _userRepository;
        private readonly IMapper _mapper;

        public TenantsController(IUserRepository<Tenant> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {

            var users = _mapper.Map<List<TenantDto>>(_userRepository.GetAllUser());

            return Ok(users);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetUserById(int id)
        {
            if (!_userRepository.UserExist(id))
                return NotFound();

            var user = _mapper.Map<TenantDto>(_userRepository.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        /// <summary>
        /// Create Tenant
        /// </summary>
        /// <param name="userCreate"></param>
        /// <remarks>
        /// "email": "tenant@gmail.com",
        /// "username": "tenant",
        /// "password": "123456",
        /// "role": 2
        /// </remarks>
        /// <returns></returns>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] TenantDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<Tenant>(userCreate);
            userMap.Role = 3;
            userMap.Status = 0;
            userMap.Address = "";
            userMap.FullName = "";
            userMap.Date_Of_Birth = DateTime.UtcNow;
            userMap.Phone_Number = "";
            // Hash và gán mật khẩu vào đối tượng Tenant
            userMap.Password = LoginRegisterController.HashPassword(userMap.Password);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateUser(int id, [FromBody] TenantDto updatedUser)
        {
            if (!_userRepository.UserExist(id))
                return NotFound();
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (id != updatedUser.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<Tenant>(updatedUser);
            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating User!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.UserExist(id))
            {
                return NotFound();
            }

            var userToDelete = _userRepository.GetUserById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User!");
            }

            return NoContent();
        }

    }
}
