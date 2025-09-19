
using ECommerceAPI.Dtos.UserDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.UserValidators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be mepty")
            .MaximumLength(25).WithMessage("Name cannot be long 25 character");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .MaximumLength(25).WithMessage("Phone numver cannot be long 25 character");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MaximumLength(25).WithMessage("Password cannot be long 25 character");

        RuleFor(x => x.UserType)
            .IsInEnum().WithMessage("Value has a range of values which does not include 3");
            

    }
}