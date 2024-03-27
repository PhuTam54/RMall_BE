using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeedbacksController : Controller
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbacksController(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllFeedBack()
        {

            var feedbacks = _mapper.Map<List<FeedbackDto>>(_feedbackRepository.GetAllFeedback());

            return Ok(feedbacks);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Feedback))]
        [ProducesResponseType(400)]
        public IActionResult GetFeedbackById(int id)
        {
            if (!_feedbackRepository.FeedbackExist(id))
                return NotFound();

            var feedback = _mapper.Map<FeedbackDto>(_feedbackRepository.GetFeedbackById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(feedback);
        }

        /// <summary>
        /// Create Feedback
        /// </summary>
        /// <param name="feedbackCreate"></param>
        /// <returns></returns>
        /// <remarks>
        /// POST/Feedback
        /// {
        /// "name": "Quan",
        /// "email": "quan123@gmail.com",
        /// "phone": "0987654321",
        /// "message": "Nice!"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFeedback([FromBody] FeedbackDto feedbackCreate)
        {
            if (feedbackCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedbackMap = _mapper.Map<Feedback>(feedbackCreate);


            if (!_feedbackRepository.CreateFeedback(feedbackMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFeedback(int id, [FromBody] FeedbackDto updatedFeedback)
        {
            if (!_feedbackRepository.FeedbackExist(id))
                return NotFound();
            if (updatedFeedback == null)
                return BadRequest(ModelState);

            if (id != updatedFeedback.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var feedbackMap = _mapper.Map<Feedback>(updatedFeedback);
            if (!_feedbackRepository.UpdateFeedback(feedbackMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Feedback!");
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
        public IActionResult DeleteFeedback(int id)
        {
            if (!_feedbackRepository.FeedbackExist(id))
            {
                return NotFound();
            }

            var feedbackToDelete = _feedbackRepository.GetFeedbackById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_feedbackRepository.DeleteFeedback(feedbackToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Feedback!");
            }

            return NoContent();
        }
    }
}
