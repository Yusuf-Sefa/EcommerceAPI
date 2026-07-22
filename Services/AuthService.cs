
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Dtos.TokenDtos;
using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Enums;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceAPI.Services;

public class AuthService : IAuthService
{
    private readonly IEnumerableRepository<User> E_repository;
    private readonly IQueryableRepository<User> Q_repository;
    private readonly IValidator<AuthUserDto> _authValidator;
    private readonly IValidator<LoginUserDto> _loginValidator;
    private readonly IMapper _mapper;
    private readonly IConfiguration configuration;
    private readonly IPasswordHasher<User>  _passwordHasher;


    public AuthService(IEnumerableRepository<User> E_repository,
                        IQueryableRepository<User> Q_repository,
                        IValidator<AuthUserDto> _authValidator,
                        IValidator<LoginUserDto> _loginValidator,
                        IMapper _mapper,
                        IConfiguration configuration,
                        IPasswordHasher<User> _passwordHasher)
    {
        this.E_repository = E_repository;
        this.Q_repository = Q_repository;
        this._authValidator = _authValidator;
        this._loginValidator = _loginValidator;
        this._mapper = _mapper;
        this.configuration = configuration;
        this._passwordHasher = _passwordHasher;
    }

    public async Task<ResponseUserDto?> RegisterAsync (AuthUserDto request)
    {
        var validationResponse = await _authValidator.ValidateAsync(request);
        if (!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());

        if(await Q_repository.Q_GetAll().AnyAsync(u => u.Email == request.Email))
            return null;

        var userEntity = _mapper.Map<User>(request);

        var hashedPassword = _passwordHasher.HashPassword(userEntity, request.Password);

        userEntity.Password = hashedPassword;
        userEntity.UserType = UserTypes.Customer;

        await E_repository.E_AddEntity(userEntity);

        //return _mapper.Map<ResponseUserDto>(userEntity);
        return await Q_repository
                        .Q_GetByFilter(u => u.Id == userEntity.Id)
                        .ProjectTo<ResponseUserDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<ResponseTokenDto?> LoginAsync (LoginUserDto request)
    {
        var validationResponse = await _loginValidator.ValidateAsync(request);
        if(!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());
        
        var user = await Q_repository.Q_GetByFilter(u => u.Email == request.Email).FirstOrDefaultAsync();

        if(user is null)
            return null;
            
        if(_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password)
            == PasswordVerificationResult.Failed)
            return null;

        return await CreateResponseToken(user);
    }
    
    public async Task<ResponseTokenDto?> RefreshTokenAsync(ResponseRefreshTokenDto request)
    {
        var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        
        return user is null
                ? null
                : await CreateResponseToken(user);
    }
    private async Task<User?> ValidateRefreshTokenAsync(int id, string refreshToken)
    {
        var user = await Q_repository.Q_GetWithInclude(u => u.Id == id).FirstOrDefaultAsync();
        if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return null;

        return user;
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        await Q_repository.Q_GetByFilter(u => u.Id == user.Id)
                            .ExecuteUpdateAsync(u => u
                                                    .SetProperty(u => u.RefreshToken, refreshToken)
                                                    .SetProperty(u => u.RefreshTokenExpiryTime, DateTime.UtcNow.AddDays(7))
                                                );
        
        return refreshToken;
    }
    private async Task<ResponseTokenDto> CreateResponseToken(User? user)
    {
        return new ResponseTokenDto
        {
            AccessToken = CreateToken(user),
            Refreshtoken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserType.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

    }

}