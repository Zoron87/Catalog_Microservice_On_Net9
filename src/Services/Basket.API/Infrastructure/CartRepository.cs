using Basket.API.Models;
using Marten;

namespace Basket.API.Infrastructure;

public class CartRepository (IDocumentSession session) : ICartRepository
{
    public async Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken ct)
    {
        var cart = await session.LoadAsync<ShoppingCart>(accountName, ct);
        if (cart is null)
            throw new ArgumentNullException($"Корзины для {accountName} не найдено");

        return cart;
    }

    public async Task<bool> RemoveCartAsync(string accountName, CancellationToken ct)
    {
        var cart = await session.LoadAsync<ShoppingCart>(accountName, ct);
        if (cart is null)
            throw new ArgumentNullException($"Корзины для {accountName} не найдено");

        session.Delete<ShoppingCart>(accountName);
        await session.SaveChangesAsync(ct);
        return true;
    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart shoppingCart, CancellationToken ct)
    {
        session.Store(shoppingCart);
        await session.SaveChangesAsync(ct);
        return shoppingCart;
    }
}
