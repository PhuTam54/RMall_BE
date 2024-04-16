using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Repositories.MovieRepositories;
using static System.Net.Mime.MediaTypeNames;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : Controller
    {
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IFoodRepository _foodRepository;

        public ShowsController(IShowRepository showRepository, IMapper mapper, IMovieRepository movieRepository, IRoomRepository roomRepository, IFoodRepository foodRepository)
        {
            _showRepository = showRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _roomRepository = roomRepository;
            _foodRepository = foodRepository;
        }

		[HttpGet]
		public IActionResult GetAllShow()
		{

			var shows = _showRepository.GetAllShow();

			return Ok(shows);
		}

		[HttpGet]
		[Route("timeNow")]
		public IActionResult GetAllShowing(DateTime timeNow)
		{
			var shows = _showRepository.GetShowing(timeNow);
			return Ok(shows);
		}

		[HttpGet]
        [Route("id")]
        public IActionResult GetShowById(int id)
        {
            if (!_showRepository.ShowExist(id))
                return NotFound();

            var show = _mapper.Map<ShowDto>(_showRepository.GetShowById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(show);
        }

        [HttpGet]
        [Route("movieId")]
        public IActionResult GetShowByMovieId(int movieId)
        {
            if (!_movieRepository.MovieExist(movieId))
                return NotFound("Movie Not Found!");

            var shows = _mapper.Map<List<ShowDto>>(_showRepository.GetShowByMovieId(movieId));

            return Ok(shows);
        }

        /// <summary>
        /// Create Show
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="roomId"></param>
        /// <param name="showCreate"></param>
        /// "Start_Date": "Same"
        /// {
        /// "show_Code": "ThisIsACode"
        /// }
        /// <returns></returns>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateShow([FromQuery]int movieId, [FromQuery]int roomId,[FromBody] ShowDto showCreate)
        {
            if(!_movieRepository.MovieExist(movieId))
                return NotFound("Movie Not Found");
            if(!_roomRepository.RoomExist(roomId))
                return NotFound("Room Not Found");

            if (showCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ////////////////////////////////////////////////////////////////////////////
            // Xử lí tránh xung đột thời gian của các xuất chiếu /////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////

            //var startDate = showCreate.Start_Date.ToString();
            //var showInRoomToday = _showRepository.GetTodayShowByRoomID(roomId, startDate);
            var movieInShow = _movieRepository.GetMovieById(movieId);
            //Console.WriteLine(movieInShow);

            //List<TimeSpan> emptySpaceInDay = new List<TimeSpan>();

            //DateTime startTime = showCreate.Start_Date; // Giả sử giờ bắt đầu của showCreate đã được chuyển thành DateTime
            //TimeSpan duration = TimeSpan.FromSeconds(movieInShow.Duration).TotalSeconds;

            //// Tính thời gian kết thúc của xuất chiếu hiện tại
            //DateTime endTime = startTime.Add(duration);

            //// Lặp qua tất cả các xuất chiếu trong phòng chiếu hiện tại để kiểm tra xem có xung đột lịch không
            //foreach (var item in showInRoomToday)
            //{
            //    DateTime existingStartTime = item.Movie.Start_Date;
            //    TimeSpan existingDuration = item.Movie.Duration;

            //    // Tính thời gian kết thúc của xuất chiếu hiện tại
            //    DateTime existingEndTime = existingStartTime.Add(existingDuration);

            //    // Kiểm tra xem thời gian bắt đầu hoặc kết thúc của xuất chiếu hiện tại có trùng với xuất chiếu khác không
            //    if ((startTime >= existingStartTime && startTime < existingEndTime) ||
            //        (endTime > existingStartTime && endTime <= existingEndTime))
            //    {
            //        // Xuất hiện xung đột lịch
            //        return BadRequest(ModelState); // hoặc thực hiện xử lý phù hợp với nghiệp vụ của bạn
            //    }
            //}

            // Nếu không có xung đột lịch, tiếp tục tạo xuất chiếu mới

            showCreate.Movie_Id = movieId;
            showCreate.Room_Id = roomId;
            var showMap = _mapper.Map<Show>(showCreate);
            showMap.Movie = movieInShow;
            showMap.Room = _roomRepository.GetRoomById(roomId);

            if (!_showRepository.CreateShow(showMap))
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
        public IActionResult UpdateShow(int id, [FromBody] ShowDto updatedShow)
        {
            if (!_showRepository.ShowExist(id))
                return NotFound();
            if (updatedShow == null)
                return BadRequest(ModelState);

            if (id != updatedShow.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var showMap = _mapper.Map<Show>(updatedShow);
            if (!_showRepository.UpdateShow(showMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Show!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteShow(int id)
        {
            if (!_showRepository.ShowExist(id))
            {
                return NotFound();
            }

            var showToDelete = _showRepository.GetShowById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_showRepository.DeleteShow(showToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Show!");
            }

            return NoContent();
        }
    }
}
