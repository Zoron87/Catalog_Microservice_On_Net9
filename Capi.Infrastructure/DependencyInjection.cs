using Capi.Domain.Repositories;
using Capi.Infrastructure.Data.Seed;
using Capi.Infrastructure.Repositories;
using Marten;

namespace Template.Capi.Infrastructure;
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
        services.AddScoped<ICategoryRepository, CatalogRepository>();

        return services;
    }
}
