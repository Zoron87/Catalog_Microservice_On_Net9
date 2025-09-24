namespace Capi.Domain.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken ct = default);
}
