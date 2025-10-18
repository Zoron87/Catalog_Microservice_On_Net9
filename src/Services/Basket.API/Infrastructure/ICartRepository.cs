using Basket.API.Models;

namespace Basket.API.Infrastructure;

public interface ICartRepository
{
    Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken ct);
    Task<bool> RemoveCartAsync(string accountName, CancellationToken ct);
    Task<ShoppingCart> SaveCartAsync(string accountName, CancellationToken ct);
}
