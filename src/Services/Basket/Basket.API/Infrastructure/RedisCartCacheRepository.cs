using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Infrastructure;

public class RedisCartCacheRepository(
                                    ICartRepository cartRepository,
                                    IDistributedCache distributedCache)
                                    : ICartRepository
{
    public async Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken ct)
    {
        string? cached = await distributedCache.GetStringAsync(accountName, ct);

        if (!string.IsNullOrEmpty(cached))
            return JsonSerializer.Deserialize<ShoppingCart>(cached)!;

        var cart = await cartRepository.GetCartAsync(accountName, ct);
        await distributedCache.SetStringAsync(accountName, JsonSerializer.Serialize(cart), ct);
        return cart;
    }

    public async Task<bool> RemoveCartAsync(string accountName, CancellationToken ct)
    {
        var result = await cartRepository.RemoveCartAsync(accountName, ct);
        await distributedCache.RemoveAsync(accountName, ct);
        return result;

    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart cart, CancellationToken ct)
    {
        var result = await cartRepository.SaveCartAsync(cart, ct);
        await distributedCache.SetStringAsync(cart.AccountName, JsonSerializer.Serialize(cart), ct);
        return result;
    }
}
