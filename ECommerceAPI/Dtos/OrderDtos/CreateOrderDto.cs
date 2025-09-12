
using ECommerceAPI.Entities.Enums;

namespace ECommerceAPI.Dtos.OrderDtos;

public class CreateOrderDto
{
    public string Code { get; set; }
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }

    public int? UserId { get; set; }
    public ICollection<int> ProductIds { get; set; } = [];

}
