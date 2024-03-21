using AutoMapper;
using RMall_BE.Dto;
using RMall_BE.Models;

namespace RMall_BE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>();
            CreateMap<Shop, ShopDto>();
            CreateMap<ShopDto, Shop>();
            CreateMap<Floor, FloorDto>();
            CreateMap<FloorDto, Floor>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();


            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
            CreateMap<Food, FoodDto>();
            CreateMap<FoodDto, Food>();
            CreateMap<Seat, SeatDto>();
            CreateMap<SeatDto, Seat>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
      

        }
    }
}
