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

    public async Task<bool> CreateAsync(Promo? promo, CancellationToken ct)
    {
        const string InsertPromo = """
            INSERT INTO Promo (Id, CatalogItemId, Title, Value)
            VALUES (@Id, @CatalogItemId, @Title, @Value);
        """;

        var result = await connection.ExecuteAsync(InsertPromo, promo);
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Promo? promo, CancellationToken ct)
    {
        const string UpdatePromo = """
            UPDATE Promo SET 
                Title = @Title, Value = @Value
            WHERE Id = @Id;
        """;

        var result = await connection.ExecuteAsync(UpdatePromo, promo);
        return result > 0;
    }
}
