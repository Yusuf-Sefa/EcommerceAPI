
using ECommerceAPI.Entities.Enums;

namespace ECommerceAPI.Dtos.OrderDtos;

public class ResponseOrderWithProducts
{
    public string Code { get; set; }
    public decimal TotalPrice { get; set; }
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string UserName { get; set; }
    public ICollection<string> ProductNames { get; set; } = [];

}