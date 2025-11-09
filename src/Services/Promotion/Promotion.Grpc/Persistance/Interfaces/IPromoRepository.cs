using Promotion.Grpc.Domain;

namespace Promotion.Grpc.Persistance.Interfaces;

public interface IPromoRepository
{
    Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId, CancellationToken ct);
}
