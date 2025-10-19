﻿using Basket.API.Models;

namespace Basket.API.Infrastructure;

public interface ICartRepository
{
    Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken ct);
    Task<bool> RemoveCartAsync(string accountName, CancellationToken ct);
    Task<ShoppingCart> SaveCartAsync(ShoppingCart cart, CancellationToken ct);
}
