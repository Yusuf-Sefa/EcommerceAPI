
using ECommerceAPI.Dtos.BrandDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.BrandValidators;

public class UpdateBrandValidator : AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(25).WithMessage("Name cannot be long 25 character");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code cannot be empty")
            .MaximumLength(25).WithMessage("Code cannot be long 25 character");

        RuleFor(x => x.Description)
            .MaximumLength(120).WithMessage("Description cannot be long 120 character");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("This area connot be null");

    }
}