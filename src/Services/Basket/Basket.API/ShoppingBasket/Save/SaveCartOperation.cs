using Basket.API.Infrastructure;
using Basket.API.Models;
using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Protos;

namespace Basket.API.ShoppingBasket.Save;

public static class SaveCartOperation
{
    public record SaveCartCommand(ShoppingCart Cart) : ICommand<SaveCartResult>;

    public class SaveCartCommandHandler(ICartRepository cartRepository, PromoService.PromoServiceClient promoService) 
        : ICommandHandler<SaveCartCommand, SaveCartResult>
    {
        public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken ct)
        {
            var cart = command.Cart;

            foreach (var item in cart.Items)
            {
                var getPromoRequest = new GetPromoRequest
                {
                    CatalogItemId = item.ItemId.ToString()
                };

                var discount = await promoService.GetPromoAsync(getPromoRequest, cancellationToken: ct);
                item.UnitPrice -= (decimal)discount.Value;
            }
            await cartRepository.SaveCartAsync(cart, ct);
            return new SaveCartResult(cart.AccountName);
        }
    }
}
