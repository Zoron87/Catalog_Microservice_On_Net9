using Promotion.Grpc.Domain;

namespace Promotion.Grpc.Persistance.Interfaces;

public interface IPromoRepository
{
    Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId, CancellationToken ct);
    Task<bool> CreateAsync(Promo? promo, CancellationToken ct);
    Task<bool> UpdateAsync(Promo? promo, CancellationToken ct);
    Task<bool> DeleteByCatalogItemIdAsync(Promo? promo, CancellationToken ct);
}
