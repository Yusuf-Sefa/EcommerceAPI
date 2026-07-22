
using AutoMapper;
using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AuthUserDto, User>();

        CreateMap<User, ResponseUserDto>()
                    .ForMember(dto => dto.Orders,
                                entity => entity.MapFrom(src => src.Orders.Select(o => o.Id).ToList()));
    }
}