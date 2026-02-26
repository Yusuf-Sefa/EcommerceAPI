
using ECommerceAPI.Dtos.CategoryDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.CategoryValidators;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name cannot be empty")
            .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Category code cannot be empty")
            .MaximumLength(20).WithMessage("Category code cannot exceed 20 characters");

        RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Description cannot exceed 250 characters");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Active status must be specified");
    }
}