namespace ECommerceAPI.Dtos.ProductDtos;

public class UpdateProductDto
{
    public string Name { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Description { get; set; }
    public bool IsActive {get; set;}

}