using Basket.API.Infrastructure;
using Basket.API.Models;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Save;

public static class SaveCartOperation
{
    public record SaveCartCommand(ShoppingCart Cart) : ICommand<SaveCartResult>;

    public class SaveCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<SaveCartCommand, SaveCartResult>
    {
        public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken ct)
        {
            var cart = command.Cart;
            await cartRepository.SaveCartAsync(cart, ct);
            return new SaveCartResult(cart.AccountName);
        }
    }
}
