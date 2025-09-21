using Capi.Domain.Entities;
using Capi.Domain.Repositories;
using Marten;

namespace Capi.Infrastructure.Repositories;

public class CatalogRepository
    : IBrandRepository,
    ICategoryRepository,
    ICatalogItemRepository
{
    private readonly IDocumentSession _session;
    public CatalogRepository(IDocumentSession store)
    {
        _session = store;
    }

    // IBrandRepository
    public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
    {
        return await _session.Query<Brand>().OrderBy(r => r.Title).ToListAsync();
    }

    // ICategoryRepository
    public async Task<IEnumerable<Category>> GetAllGategoriesAsync()
    {
        return await _session.Query<Category>().ToListAsync();
    }

    // ICatalogItemRepository
    public async Task<IEnumerable<CatalogItem>> GetAllCatalogItemAsync()
    {
        return await _session.Query<CatalogItem>().ToListAsync();
    }

    public Task<CatalogItem?> GetCatalogItemAsync(Guid id)
    {
        return _session.LoadAsync<CatalogItem>(id);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(string brandTitle)
    {
        return await _session.Query<CatalogItem>().Where
            (r => r.Brand != null 
                && !String.IsNullOrWhiteSpace(r.Brand.Title)
                && r.Brand.Title.Equals(brandTitle, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title)
    {
        return await _session.Query<CatalogItem>().Where
            (r => r.Brand != null
                && !String.IsNullOrWhiteSpace(r.Title)
                && r.Brand.Title!.Equals(title, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }

    public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item)
    {
        try
        {
            _session.Store(item);
            await _session.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            // logger
            throw;
        }
    }

    public async Task<bool> DeleteCatalogItemAsync(Guid id)
    {
        try
        {
            var item = _session.Query<CatalogItem>().Where(r => r.Id == id);
            _session.Delete<CatalogItem>(id);
            await _session.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // logger
            return false;
        }
    }

    public async Task<bool> UpdateCatalogItemAsync(CatalogItem item)
    {
        try
        { 
            _session.Store(item);
            await _session.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // logger
            return false;
        }
    }
}
