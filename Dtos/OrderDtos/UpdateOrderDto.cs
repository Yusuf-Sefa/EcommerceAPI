
using ECommerceAPI.Entities.Enums;

namespace ECommerceAPI.Dtos.OrderDtos;

public class UpdateOrderDto
{
    public string Code { get; set; }
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsActive { get; set; }
}