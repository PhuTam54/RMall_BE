﻿using AutoMapper;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Dto.MoviesDto.SeatsDto;
using RMall_BE.Dto.OrdersDto;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Dto.UsersDto;
using RMall_BE.Entities;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Genres;
using RMall_BE.Models.Movies.Languages;
using RMall_BE.Models.Movies.Seats;
using RMall_BE.Models.Orders;
using RMall_BE.Models.Shops;
using RMall_BE.Models.User;

namespace RMall_BE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Shop
            CreateMap<Shop, ShopDto>();
            CreateMap<ShopDto, Shop>();
            CreateMap<Floor, FloorDto>();
            CreateMap<FloorDto, Floor>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            #endregion

            #region Movie
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>();
            CreateMap<Show, ShowDto>();
            CreateMap<ShowDto, Show>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<GalleryMovie, GalleryMallDto>();
            CreateMap<GalleryMovieDto, GalleryMovie>();
            #endregion

            #region Seat
            CreateMap<Seat, SeatDto>();
            CreateMap<SeatDto, Seat>();
            CreateMap<SeatType, SeatTypeDto>();
            CreateMap<SeatTypeDto, SeatType>();
            CreateMap<SeatReservation, SeatReservationDto>();
            CreateMap<SeatReservationDto, SeatReservation>();
            #endregion

            #region Order
            CreateMap<Food, FoodDto>();
            CreateMap<FoodDto, Food>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            #endregion

            #region User
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();
            CreateMap<Tenant, TenantDto>();
            CreateMap<TenantDto, Tenant>();
            CreateMap<RegisterRequest, Customer>();
            #endregion

            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>();

            CreateMap<GalleryMallDto, GalleryMall>();
            CreateMap<GalleryMall, GalleryMallDto>();
        }
    }
}
