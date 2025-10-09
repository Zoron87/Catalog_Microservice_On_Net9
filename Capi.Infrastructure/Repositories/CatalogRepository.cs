using Capi.Domain.Entities;
using Capi.Domain.Repositories;
using Capi.Domain.Specifications;
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
    public async Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken ct)
    {
        return await _session.Query<Brand>().OrderBy(r => r.Title).ToListAsync(ct);
    }

    // ICategoryRepository
    public async Task<IEnumerable<Category>> GetAllGategoriesAsync(CancellationToken ct)
    {
        return await _session.Query<Category>().ToListAsync(ct);
    }

    // ICatalogItemRepository
    public async Task<IEnumerable<CatalogItem>> GetAllCatalogItemAsync(CancellationToken ct)
    {
        return await _session.Query<CatalogItem>().ToListAsync(ct);
    }

    public Task<CatalogItem?> GetCatalogItemAsync(Guid id, CancellationToken ct)
    {
        return _session.LoadAsync<CatalogItem>(id, ct);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(string brandTitle, CancellationToken ct)
    {
        return await _session.Query<CatalogItem>().Where
            (r => r.Brand != null 
                && !String.IsNullOrWhiteSpace(r.Brand.Title)
                && r.Brand.Title.Equals(brandTitle, StringComparison.OrdinalIgnoreCase))
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title, CancellationToken ct)
    {
        return await _session.Query<CatalogItem>().Where
            (r => r.Brand != null
                && !String.IsNullOrWhiteSpace(r.Title)
                && r.Brand.Title!.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToListAsync(ct);
    }

    public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken ct)
    {
        try
        {
            _session.Store(item);
            await _session.SaveChangesAsync(ct);
            return item;
        }
        catch (Exception ex)
        {
            // logger
            throw;
        }
    }

    public async Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var item = _session.Query<CatalogItem>().Where(r => r.Id == id);
            _session.Delete<CatalogItem>(id);
            await _session.SaveChangesAsync(ct);
            return true;
        }
        catch (Exception ex)
        {
            // logger
            return false;
        }
    }

    public async Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken ct)
    {
        try
        { 
            _session.Store(item);
            await _session.SaveChangesAsync(ct);
            return true;
        }
        catch (Exception ex)
        {
            // logger
            return false;
        }
    }

    public async Task<Pagination<CatalogItem>> GetCatalogItemsAsync(QueryArgs args, CancellationToken ct)
    {
        var allItems = _session.Query<CatalogItem>().AsQueryable();

        var brandId = args.BrandId;
        if (brandId is not null)
            allItems.Where(e => e.Brand != null
                           && e.Brand.Id == brandId);

        var categoryId = args.CategoryId;
        if (categoryId is not null)
            allItems.Where(e => e.Category != null
                           && e.Category.Id == categoryId);

        var search = args.Search;
        if (!string.IsNullOrWhiteSpace(search))
            allItems.Where(e => e.Title != null
                           && e.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(args.Sort))
        {
            allItems = args.Sort.ToLower() switch
            {
                "price_desc" => allItems.OrderByDescending(x => x.Price),
                "price_asc" => allItems.OrderBy(x => x.Price),
                "title_desc" => allItems.OrderByDescending(x => x.Title),
                "title_asc" => allItems.OrderBy(x => x.Title),
                _ => allItems
            };
        }

        var count = await allItems.CountAsync();
        var items = await allItems
            .Skip((args.PageIndex - 1) * args.PageSize)
            .Take(args.PageSize)
            .ToListAsync();

       return new Pagination<CatalogItem>(args.PageIndex, args.PageSize, count, items);
    }
}
