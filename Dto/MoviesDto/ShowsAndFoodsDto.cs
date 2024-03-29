using RMall_BE.Dto.OrdersDto;
using RMall_BE.Models.Orders;

namespace RMall_BE.Dto.MoviesDto
{
    public class ShowsAndFoodsDto
    {
        public List<ShowDto> Shows { get; set; }
        public List<FoodDto> Foods { get; set; }
    }
}
