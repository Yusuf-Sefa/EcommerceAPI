
namespace ECommerceAPI.Dtos.TokenDtos;

public class ResponseRefreshTokenDto
{
    public int UserId {get; set;}
    public required string RefreshToken {get; set;}
}