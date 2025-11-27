using Carter;
using Checkout.Infrastructure.Data.Extensions;
using Common.Kernel.Exceptions.Handler;

namespace Capi.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddCarter();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static async Task<WebApplication> UseApiServices(
        this WebApplication app
    )
    {
        app.UseExceptionHandler(options => { });
        app.MapCarter();
        app.UseSwagger();
        app.UseSwaggerUI();

        await app.Services.MigrateAndSeedDatabaseAsync();

        app.MapGet("/", () => "Hello World!");

        return app;
    }
}
