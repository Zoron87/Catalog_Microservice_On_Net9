using Capi.Domain.Entities;
using Marten;
using Marten.Schema;

namespace Capi.Infrastructure.Data.Seed;

public class InitialDataBaseAsync : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (!await session.Query<Brand>().AnyAsync()) 
        {
            session.Store<Brand>(InitialData.Brands);
        }

        foreach (var category in InitialData.Categories)
        {
            if (!session.Query<Category>().Any(c => c.Id == category.Id))
            {
                session.Store(category);
            }
        }

        foreach (var item in InitialData.CatalogItems)
        {
            if (!session.Query<Category>().Any(c => c.Id == item.Id))
            {
                session.Store(item);
            }
        }

        await session.SaveChangesAsync(cancellation);
    }
}
