using FluentValidation;
using static Basket.API.ShoppingBasket.Remove.RemoveCartOperation;

namespace Basket.API.ShoppingBasket.Remove;

public class RemoveCartCommandValidator : AbstractValidator<RemoveCartCommand>
{
    public RemoveCartCommandValidator()
    {
        RuleFor(x => x.AccountName).NotEmpty().WithMessage("AccountName не может быть пустой!")
                                   .MaximumLength(50).WithMessage("AccountName слишком длинный");
    }
}
