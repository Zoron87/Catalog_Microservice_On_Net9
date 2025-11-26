using Checkout.Domain.Common;
using Checkout.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Transactions;

namespace Checkout.Infrastructure.Data;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public OrderContext(DbContextOptions<OrderContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        HandleAuditFields();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        HandleAuditFields();
        return base.SaveChanges();
    }

    private void HandleAuditFields()
    {
        var currentUser = $"User {Guid.NewGuid().ToString().Substring(0, 5)}";
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var item in entries)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    item.Entity.SetCreatedAudit(currentUser);
                    break;
                case EntityState.Modified:
                    item.Entity.SetModifiedAudit(currentUser);
                    break;
            }
        }
    }
}
