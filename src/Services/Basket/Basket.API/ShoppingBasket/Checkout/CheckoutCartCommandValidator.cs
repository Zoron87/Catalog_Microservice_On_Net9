using FluentValidation;

namespace Basket.API.ShoppingBasket.Checkout;

public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x.AccountName).NotEmpty().Length(3, 20).WithMessage("AccountName должен быть от 3 до 20 символов");
        RuleFor(x => x.FirstName).NotEmpty().Length(100).WithMessage("Имя не должно превышать 100 символов");
        RuleFor(x => x.LastName).NotEmpty().Length(100).WithMessage("Фамилия не должно превышать 100 символов");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Некорректный email адрес");
        RuleFor(x => x.Street).NotEmpty().MaximumLength(200).WithMessage("Адрес обязателен");

        When(x => x.PaymentMethod == 1, () => // BankTransfer
        {
            RuleFor(x => x.CardName).Null();
            RuleFor(x => x.CardNumber).Null();
            RuleFor(x => x.Expiration).Null();
            RuleFor(x => x.Cvv).Null();
        });
    }
}
