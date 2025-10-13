
namespace ECommerceAPI.Dtos.CategoryDtos;

public class UpdateCategoryDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}