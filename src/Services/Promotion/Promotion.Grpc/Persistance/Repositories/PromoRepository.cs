using Dapper;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistance.Interfaces;
using System.Data;

namespace Promotion.Grpc.Persistance.Repositories;

public class PromoRepository (IDbConnection connection) : IPromoRepository
{
    public async Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(catalogItemId);
        const string SelectByCatalogItemId = """
            SELECT * FROM Promo
            WHERE catalogItemId = @catalogItemId
            LIMIT 1;
        """;

        var result = await connection.QueryFirstOrDefaultAsync<Promo>(SelectByCatalogItemId, 
                                                                      new { CatalogItemId = catalogItemId});
        return result;
    }
}
