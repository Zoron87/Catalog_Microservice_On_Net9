using FluentValidation;

namespace Checkout.Application.Orders.Commands.ProcessOrderSubmission;

public class ProcessOrderSubmissionCommandValidator : AbstractValidator<ProcessOrderSubmissionCommand>
{
    public ProcessOrderSubmissionCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId обязателен");
        RuleFor(x => x.AccountName).NotEmpty().Length(3, 20).WithMessage("AccountName должен быть от 3 до 20 символов");
        RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("TotalPrice должен быть больше 0");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Items не может быть пустым");
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.Quantity).GreaterThan(0);
            item.RuleFor(x => x.UnitPrice).GreaterThan(0);
        });
    }
}
