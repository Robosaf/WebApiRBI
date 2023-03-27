using AutoMapper;
using WebApiRBI.Dto;
using WebApiRBI.Models;

namespace WebApiRBI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<RestaurantName, RestaurantNameDto>();
            CreateMap<RestaurantNameDto, RestaurantName>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
