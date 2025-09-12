namespace ECommerceAPI.Entities;

public class Brand
{
    //Entity Attributes//
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false; //To soft delete//
    public string? DeletedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }


    //Navigation Attributes//
    public ICollection<Product>? Products { get; } = [];

}