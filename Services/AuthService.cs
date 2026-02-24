
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Repository.RepositoryInterfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceAPI.Services;

public class AuthService
{
    private readonly IEnumerableRepository<User> E_repository;
    private readonly IQueryableRepository<User> Q_repository;
    private readonly IValidator<AuthUserDto> _validator;
    private readonly IMapper _mapper;
    private readonly IConfiguration configuration;
    public AuthService(User user,
                        IEnumerableRepository<User> E_repository,
                        IQueryableRepository<User> Q_repository,
                        IValidator<AuthUserDto> _validator,
                        IMapper _mapper,
                        IConfiguration configuration)
    {
        this.E_repository = E_repository;
        this.Q_repository = Q_repository;
        this._validator = _validator;
        this._mapper = _mapper;
        this.configuration = configuration;
    }

    public async Task<ResponseUserDto> Register (AuthUserDto request)
    {
        var validationResponse = await _validator.ValidateAsync(request);
        if (!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());

        var userEntity = _mapper.Map<User>(request);

        var hashedPassword = new PasswordHasher<User>()
                                    .HashPassword(userEntity, request.Password);

        userEntity.Password = hashedPassword;

        return _mapper.Map<ResponseUserDto>(userEntity);
    }

    public async Task<string> Login (LoginUserDto request)
    {
        var validationResponse = await _validator.ValidateAsync(request);
        if(!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());
        
        var user = await Q_repository.Q_GetByFilter(u => u.Email == request.Email).FirstOrDefaultAsync();

        if(user is null)
            throw new Exception("Wrong e-mail or password");
            
        if(new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password)
            == PasswordVerificationResult.Failed)
            throw new Exception("Wrong e-mail or password");

        string token = CreateToken(user);

        return token;
    }

    public string CreateToken(User user)
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