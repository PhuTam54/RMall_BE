using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Helpers;
using RMall_BE.Identity;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllReview()
        {

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReview());

            return Ok(reviews);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetReviewById([FromQuery] int id)
        {
            if (!_reviewRepository.ReviewExist(id))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReviewById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpPost]
        public IActionResult CreateReview([FromQuery] int userId, [FromQuery] int movieId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Created_At = DateTime.Now;

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateReview([FromQuery] int id, [FromBody] ReviewDto updatedReview)
        {
            if (!_reviewRepository.ReviewExist(id))
                return NotFound();
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (id != updatedReview.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(updatedReview);
            reviewMap.Updated_At = DateTime.Now;

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Review!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpPut]
        [Route("trash")]
        public IActionResult MoveReviewToTrash([FromQuery] int id)
        {
            if (!_reviewRepository.ReviewExist(id))
            {
                return NotFound();
            }

            var reviewToDelete = _reviewRepository.GetReviewById(id);
            reviewToDelete.Deleted_At = DateTime.Now;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.UpdateReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Review!");
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpPut]
        [Route("restore")]
        public IActionResult RestoreReviewFromTrash([FromQuery] int id)
        {
            if (!_reviewRepository.ReviewExist(id))
            {
                return NotFound();
            }

            var reviewToDelete = _reviewRepository.GetReviewById(id);
            reviewToDelete.Deleted_At = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.UpdateReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Review!");
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteReview([FromQuery] int id)
        {
            if (!_reviewRepository.ReviewExist(id))
            {
                return NotFound();
            }

            var reviewToDelete = _reviewRepository.GetReviewById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Review!");
            }

            return NoContent();
        }
    }
}
