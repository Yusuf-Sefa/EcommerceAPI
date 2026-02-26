

using ECommerceAPI.Dtos.UserDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.UserValidators;

public class AuthUserValidator : AbstractValidator<AuthUserDto>
{
    public AuthUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username cannot be empty")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("A valid email address is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10,15}$").WithMessage("Invalid phone number format");

        RuleFor(x => x.UserType)
            .IsInEnum().WithMessage("Invalid user type");
    }
}