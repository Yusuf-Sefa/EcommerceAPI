using ECommerceAPI.Entities.Enums;
using ECommerceAPI.Entities.PivotTables;

namespace ECommerceAPI.Entities;

public class Order
{
    //Entity Attributes
    public int Id { get; set; }
    public string Code { get; set; }
    public decimal TotalPrice { get; set; } 
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    //Navigation Attributes
    public User? User { get; set; }
    public int? UserId { get; set; }

    public ICollection<ProductOrder>? ProductOrders { get; } = [];

}