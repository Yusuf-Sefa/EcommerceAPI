
using AutoMapper;
using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, User>();
        
        CreateMap<User, ResponseUserDto>();
    }
}