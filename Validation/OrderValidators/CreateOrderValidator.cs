
using ECommerceAPI.Dtos.OrderDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.OrderValidators;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code cannot be empty")
            .MaximumLength(25).WithMessage("Code cannot be long 25 character");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Address cannot be empty")
            .MaximumLength(50).WithMessage("Address cannot be long 50 character");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Value has a range of values which does not include 4");

        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User id cannot be null")
            .GreaterThan(0).WithMessage("User id must be positive");

        RuleFor(x => x.ProductIds)
            .NotEmpty().WithMessage("Product Ids cannot be empty");

        RuleForEach(x => x.ProductIds)
            .NotNull().WithMessage("Product id cannot be null")
            .GreaterThan(0).WithMessage("Id must be positive");

    }
}