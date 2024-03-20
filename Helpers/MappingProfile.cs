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
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
