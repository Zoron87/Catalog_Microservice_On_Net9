using Checkout.Domain.Common;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Checkout.Infrastructure.Repositories;

public abstract class BaseRepository<T>(OrderContext context) : IRepository<T> where T : class, IAggregateRoot
{
    protected readonly OrderContext dbContext = context;

    // IReadRepository
    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await dbContext.Set<T>().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    // IWriteRepository
    public virtual async Task<bool> UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        return await dbContext.SaveChangesAsync() > 0;
    }
}
