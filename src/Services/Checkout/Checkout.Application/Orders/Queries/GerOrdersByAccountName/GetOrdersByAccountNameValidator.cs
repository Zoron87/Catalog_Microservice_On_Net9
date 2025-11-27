using FluentValidation;

namespace Checkout.Application.Orders.Queries.GerOrdersByAccountName;

public class GetOrdersByAccountNameValidator : AbstractValidator<GetOrdersByAccountNameQuery>
{
    public GetOrdersByAccountNameValidator ()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage("Имя аккаунта обязательно для заполнения")
            .NotNull()
            .WithMessage("Имя аккаунта не может быть пустым")
            .MinimumLength(3)
            .WithMessage("Имя аккаунта должно содержать минимум 3 символа")
            .MinimumLength(20)
            .WithMessage("Имя аккаунта не может превышать 20 символов")
            .Matches("^[a-zA-Z0-9_@.-]+$")
            .WithMessage("Имя аккаунта может содержать только буквы, цифры, подчеркивание, @, точку и дефис");
    }
}
