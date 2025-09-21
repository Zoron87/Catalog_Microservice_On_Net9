namespace Capi.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllGategoriesAsync();
}
