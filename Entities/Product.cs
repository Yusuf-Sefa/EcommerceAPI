
using ECommerceAPI.Entities.Interfaces;
using ECommerceAPI.Entities.PivotTables;

namespace ECommerceAPI.Entities;

public class Product : IEntityIdBase
{
    //Entity Attributes
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false; //To soft delete//
    public string? DeletedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }


    //Navigation Attributes
    public Brand? Brand { get; set; }
    public int? BrandId { get; set; }

    public ICollection<ProductCategory>? ProductCategories { get; } = [];

    public ICollection<ProductOrder>? ProductOrders { get; } = [];
}