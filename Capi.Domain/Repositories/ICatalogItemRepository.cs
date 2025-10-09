using Capi.Domain.Specifications;

namespace Capi.Domain.Repositories;

public interface ICatalogItemRepository
{
    Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken ct = default);
    Task<IEnumerable<CatalogItem>> GetAllCatalogItemAsync(CancellationToken ct = default);
    Task<CatalogItem?> GetCatalogItemAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title, CancellationToken ct = default);
    Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(string brandTitle, CancellationToken ct = default);
    Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken ct = default);
    Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken ct = default);
    Task<Pagination<CatalogItem>> GetCatalogItemsAsync(QueryArgs args, CancellationToken ct = default);
}
