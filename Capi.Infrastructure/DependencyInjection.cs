using Capi.Infrastructure.Data.Seed;
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

        return services;
    }
}
