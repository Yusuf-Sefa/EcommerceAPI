
using ECommerceAPI.Dtos.CategoryDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.CategoryValidators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(25).WithMessage("Name cannot be long 25 character");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code cannot be empty")
            .MaximumLength(25).WithMessage("Code cannot be long 25 character");

        RuleFor(x => x.Description)
            .MaximumLength(120).WithMessage("Description cannot be long 120 character");

    }
}