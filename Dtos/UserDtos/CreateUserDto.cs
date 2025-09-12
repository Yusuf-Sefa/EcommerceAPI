
using ECommerceAPI.Entities.Enums;

namespace ECommerceAPI.Dtos.UserDtos;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public UserTypes UserType { get; set; }
}