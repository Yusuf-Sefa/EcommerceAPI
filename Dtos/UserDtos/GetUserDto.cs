
namespace ECommerceAPI.Dtos.UserDtos;

public class GetUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<int> Orders { get; set; } = new List<int>();
}