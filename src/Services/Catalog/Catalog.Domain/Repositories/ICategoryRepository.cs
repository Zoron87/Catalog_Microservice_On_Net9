namespace Catalog.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllGategoriesAsync(CancellationToken ct = default);
}
