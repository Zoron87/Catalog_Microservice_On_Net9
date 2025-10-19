using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<SaveCartCommand, SaveCartResult>
{
    public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken ct)
    {
        var cart = command.Cart;
        await cartRepository.SaveCartAsync(cart, ct);
        return new SaveCartResult(cart.AccountName);
    }
}
