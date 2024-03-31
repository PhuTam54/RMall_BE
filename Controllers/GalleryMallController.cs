using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MallInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GalleryMallController : Controller
    {
        private readonly IGalleryMallRepository _GalleryMallRepository;
        private readonly IMapper _mapper;

        public GalleryMallController(IGalleryMallRepository GalleryMallRepository, IMapper mapper)
        {
            _GalleryMallRepository = GalleryMallRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllGalleryMall()
        {
            var GalleryMalls = _mapper.Map<List<GalleryMallDto>>(_GalleryMallRepository.GetAllGalleryMall());

            return Ok(GalleryMalls);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetGalleryMallById(int id)
        {
            if (!_GalleryMallRepository.GalleryMallExist(id))
                return NotFound();

            var GalleryMall = _mapper.Map<GalleryMallDto>(_GalleryMallRepository.GetGalleryMallById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(GalleryMall);
        }

        /// <summary>
        /// Create GalleryMall
        /// </summary>
        /// <param name="GalleryMallCreate"></param>
        /// <returns></returns>
        /// <remarks>
        /// POST/GalleryMall
        /// {
        /// "image_Path": "Mall.png",
        /// "product_Name": "Mall",
        /// "description": "Mall Center"
        /// }
        /// </remarks>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateGalleryMall([FromBody] GalleryMallDto GalleryMallCreate)
        {
            if (GalleryMallCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var GalleryMallMap = _mapper.Map<GalleryMall>(GalleryMallCreate);

            if (!_GalleryMallRepository.CreateGalleryMall(GalleryMallMap))
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
        public IActionResult UpdateGalleryMall(int id, [FromBody] GalleryMallDto updatedGalleryMall)
        {
            if (!_GalleryMallRepository.GalleryMallExist(id))
                return NotFound();
            if (updatedGalleryMall == null)
                return BadRequest(ModelState);

            if (id != updatedGalleryMall.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var GalleryMallMap = _mapper.Map<GalleryMall>(updatedGalleryMall);
            if (!_GalleryMallRepository.UpdateGalleryMall(GalleryMallMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Gallery Movie!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteGalleryMall(int id)
        {
            if (!_GalleryMallRepository.GalleryMallExist(id))
            {
                return NotFound();
            }

            var GalleryMallToDelete = _GalleryMallRepository.GetGalleryMallById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_GalleryMallRepository.DeleteGalleryMall(GalleryMallToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Gallery Movie!");
            }

            return NoContent();
        }
    }
}
