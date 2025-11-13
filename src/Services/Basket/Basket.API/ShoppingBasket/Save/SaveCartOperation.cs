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
            await ApplyPromotionsToCartAsync(cart, ct);
            await cartRepository.SaveCartAsync(cart, ct);
            return new SaveCartResult(cart.AccountName);
        }

        private async Task ApplyPromotionsToCartAsync(ShoppingCart cart, CancellationToken ct)
        {
            foreach (var item in cart.Items)
            {
                PromoModel discount = await GetDiscountForItemAsync(item, ct);
                item.UnitPrice -= (decimal)discount.Value;
            }
        }

        private async Task<PromoModel> GetDiscountForItemAsync(ShoppingCartItem item, CancellationToken ct)
        {
            var getPromoRequest = new GetPromoRequest
            {
                CatalogItemId = item.ItemId.ToString()
            };

            var discount = await promoService.GetPromoAsync(getPromoRequest, cancellationToken: ct);
            return discount;
        }
    }
}
