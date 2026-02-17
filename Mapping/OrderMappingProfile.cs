
using AutoMapper;
using ECommerceAPI.Dtos.OrderDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();

        CreateMap<Order, ResponseOrderDto>();
        CreateMap<Order, ResponseOrderWithProducts>()
                    .ForMember(dto => dto.ProductNames,
                                entity => entity.MapFrom(src => src.ProductOrders
                                                                    .Select(po => po.Product.Name)));
    }
}