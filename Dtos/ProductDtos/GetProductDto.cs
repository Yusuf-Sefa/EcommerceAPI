
namespace ECommerceAPI.Dtos.ProductDtos;

public class GetProductDto
{
    public string Name { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string BrandName { get; set; }
        
}