using AutoMapper;
using Users.Models.DTOs;
using Users.Models.Entities;

namespace Users.API.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<GeoCoordinate, GeoCoordinateDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<User, UserWithLinksDto>();

            CreateMap<UserDto, User>();
            CreateMap<AddressDto, Address>();
            CreateMap<GeoCoordinateDto, GeoCoordinate>();
            CreateMap<CompanyDto, Company>();

            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
