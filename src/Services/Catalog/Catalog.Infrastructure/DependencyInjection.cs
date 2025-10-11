using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data.Seed;
using Catalog.Infrastructure.Repositories;
using Marten;

namespace Catalog.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PgConnection")!;
        services.AddMarten(options =>
        {
            options.Connection(connectionString);
        }).UseLightweightSessions()
        .InitializeWith<InitialDataBaseAsync>();

        services.AddScoped<IBrandRepository, CatalogRepository>();
        services.AddScoped<ICategoryRepository, CatalogRepository>();
        services.AddScoped<ICatalogItemRepository, CatalogRepository>();

        return services;
    }
}
