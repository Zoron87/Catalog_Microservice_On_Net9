using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Repositories;

public class OrderRepository(OrderContext context) : BaseRepository<Order>(context), IOrderRepository
{
    public override async Task<IReadOnlyList<Order>> GetAllAsync()
    {
        return await dbContext.Orders.Include(x => x.Items).AsNoTracking().ToListAsync();
    }

    public override async Task<Order?> GetByIdAsync(Guid id)
    {
        return await dbContext.Orders.Include(x => x.Items).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByAccountName(string accountName)
    {
        return  await dbContext.Orders.Include(x =>x.Items).AsNoTracking().
                                          Where(e => e.AccountName == accountName)
                                          .ToListAsync();
    }
}
