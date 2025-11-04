using FluentValidation;
using static Basket.API.ShoppingBasket.Save.SaveCartOperation;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartCommandValidator : AbstractValidator<SaveCartCommand>
{
    public SaveCartCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Не может быть null");
        RuleFor(x => x.Cart.AccountName).NotEmpty()
            .WithMessage("AccountName обязателен").MaximumLength(100).WithMessage("AccountName слишком длинный");
        RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("Корзина не может быть пустой");

        RuleForEach(x => x.Cart.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ItemId).NotEqual(Guid.Empty).WithMessage("ItemId должен быть валидным Guid");
            item.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("UnitPrice должен быть больше 0");
            item.RuleFor(i => i.ItemTitle).NotEmpty().WithMessage("ItemTitle обязателен");
        });
    }
}
