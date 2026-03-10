
using ECommerceAPI.Dtos.OrderDtos;
using FluentValidation;

namespace ECommerceAPI.Validation.OrderValidators;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Order code cannot be empty");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required")
            .MinimumLength(10).WithMessage("Please provide a more detailed address");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid order status");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Active status must be specified");
    }
}