using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Repositories.MovieRepositories;
using RMall_BE.Repositories.MovieRepositories.SeatRepositories;
using RMall_BE.Repositories.OrderRepositories;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IOrderRepository _orderRepository;

        public BookingsController(IMapper mapper, IRoomRepository roomRepository ,IFoodRepository foodRepository, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _foodRepository = foodRepository;
            _orderRepository = orderRepository;
        }

        //[HttpGet]
        //public IActionResult GetBooking()
        //{

        //}

    }
}
