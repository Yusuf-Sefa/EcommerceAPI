using ECommerceAPI.Entities.Enums;
using ECommerceAPI.Entities.Interfaces;

namespace ECommerceAPI.Entities;

public class User : IEntityIdBase
{
    //Entity Attributes
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false; //To soft delete//
    public string? DeletedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    //Navigation Attributes
    public UserTypes UserType { get; set; }

    public ICollection<Order>? Orders { get; } = [];

}