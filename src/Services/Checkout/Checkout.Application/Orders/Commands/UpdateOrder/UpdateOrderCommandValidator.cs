using FluentValidation;

namespace Checkout.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.UpdateOrderData.OrderId).NotEmpty().WithMessage("ID заказа обязателен");
        RuleFor(x => x.UpdateOrderData).NotNull().WithMessage("Данные заказа для обновления не могут быть пустыми");
    }
}
