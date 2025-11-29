using FluentValidation;

namespace Checkout.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderData).NotNull();
        RuleFor(x => x.OrderData.AccountName).NotEmpty().Length(3, 20);

        RuleFor(x => x.OrderData.Items).NotEmpty();
        RuleForEach(x => x.OrderData.Items).ChildRules(i =>
        {
            i.RuleFor(x => x.Quantity).GreaterThan(0);
            i.RuleFor(x => x.UnitPrice).GreaterThan(0);
        });
    }
}
