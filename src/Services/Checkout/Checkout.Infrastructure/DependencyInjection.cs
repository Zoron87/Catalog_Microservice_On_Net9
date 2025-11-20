using Checkout.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Capi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var pgConnection = configuration.GetConnectionString("PgConnection");
        services.AddDbContext<OrderContext>(options =>
            options.UseNpgsql(pgConnection));
        return services;
    }
}
