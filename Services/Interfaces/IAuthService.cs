
using ECommerceAPI.Dtos.TokenDtos;
using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Services.Interfaces;

public interface IAuthService
{
    Task<ResponseUserDto?> RegisterAsync(AuthUserDto request);
    Task<ResponseTokenDto?> LoginAsync(LoginUserDto request);
    Task<ResponseTokenDto?> RefreshTokenAsync(ResponseRefreshTokenDto request);
}