
using ECommerceAPI.Dtos.ProductDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.ProductValidators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(25).WithMessage("Name cannot be long 25 character");

        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("SKU cannot be empty")
            .MaximumLength(25).WithMessage("SKU cannot be long 25 character");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price cannot be empty")
            .GreaterThan(0).WithMessage("Price must be positive");

        RuleFor(x => x.StockQuantity)
            .GreaterThan(-1).WithMessage("Stock cannot be negative");

        RuleFor(x => x.Description)
            .MaximumLength(50).WithMessage("Description cannot be long 50 character"); ;

        RuleFor(x => x.BrandId)
            .NotNull().WithMessage("Brand id cannot be null")
            .GreaterThan(0).WithMessage("Brand id must be positive");

        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("Category ids cannot be empty");

        RuleForEach(x => x.CategoryIds)
            .NotNull().WithMessage("Category id cannot be null")
            .GreaterThan(0).WithMessage("Category id must be positive");

    }
}