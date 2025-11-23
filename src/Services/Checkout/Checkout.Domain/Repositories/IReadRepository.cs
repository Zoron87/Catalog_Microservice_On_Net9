using Checkout.Domain.Common;
using System.Linq.Expressions;

namespace Checkout.Domain.Repositories;

public interface IReadRepository<T> where T : IAggregateRoot
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
}
