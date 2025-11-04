using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Queries;

namespace Basket.API.ShoppingBasket.Cart;

public static class RetrieveCartOperation
{
    public record RetrieveCartQuery(string AccountName) : IQuery<RetrieveCartResult>;

    public class RetrieveCartQueryHandler(ICartRepository cartRepository) : IQueryHandler<RetrieveCartQuery, RetrieveCartResult>
    {
        public async Task<RetrieveCartResult> Handle(RetrieveCartQuery query, CancellationToken ct)
        {
            var currentCart = await cartRepository.GetCartAsync(query.AccountName, ct);
            return new RetrieveCartResult(currentCart);
        }
    }
}
