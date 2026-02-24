
using ECommerceAPI.Entities.Enums;

namespace ECommerceAPI.Dtos.UserDtos;

public class AuthUserDto
{
    public string UserName {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public UserTypes UserType { get; set; }
}