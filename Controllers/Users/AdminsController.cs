using AutoMapper;
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
    public class AdminsController : Controller
    {
        private readonly IUserRepository<Admin> _userRepository;
        private readonly IMapper _mapper;

        public AdminsController(IUserRepository<Admin> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {

            var users = _mapper.Map<List<AdminDto>>(_userRepository.GetAllUser());

            return Ok(users);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Admin))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(int id)
        {
            if (!_userRepository.UserExist(id))
                return NotFound();

            var user = _mapper.Map<AdminDto>(_userRepository.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [AllowAnonymous]
        //[Authorize]
        //[RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] AdminDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<Admin>(userCreate);
            // Hash và gán mật khẩu vào đối tượng Admin
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
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int id, [FromBody] AdminDto updatedUser)
        {
            if (!_userRepository.UserExist(id))
                return NotFound();
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (id != updatedUser.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<Admin>(updatedUser);
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
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
