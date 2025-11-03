using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Remove;

public static class RemoveCartOperation
{
    public record RemoveCartCommand(string AccountName) : ICommand<RemoveCartResult>;

    public class RemoveCartHandler(ICartRepository cartRepository) : ICommandHandler<RemoveCartCommand, RemoveCartResult>
    {
        public async Task<RemoveCartResult> Handle(RemoveCartCommand command, CancellationToken ct)
        {
            var result = await cartRepository.RemoveCartAsync(command.AccountName, ct);

             return new RemoveCartResult(result);
        }
    }
}
