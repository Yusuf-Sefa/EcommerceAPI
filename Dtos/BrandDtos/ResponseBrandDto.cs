
namespace ECommerceAPI.Dtos.BrandDtos;

public class ResponseBrandDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}