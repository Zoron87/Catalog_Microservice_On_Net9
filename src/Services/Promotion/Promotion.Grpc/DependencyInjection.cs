using Common.Logging.Extensions;
using MySqlConnector;
using Prometheus;
using Promotion.Grpc.Configuration;
using Promotion.Grpc.Interceptors;
using Promotion.Grpc.Persistance.Extensions;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Persistance.Repositories;
using Promotion.Grpc.Services;
using System.Data;

namespace Promotion.Grpc;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("MySqlConnection");
        services.AddScoped<IDbConnection>(_ =>
                new MySqlConnection(mySqlConnection));

        services.AddSingleton<RequestLoggingInterceptor>();
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<RequestLoggingInterceptor>();
            options.EnableDetailedErrors = true;
        });
        services.AddCommonLogging(configuration);
        services.AddGrpcReflection();

        var assembly = typeof(Program).Assembly;
        var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;

        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<IPromoRepository, PromoRepository>();

        MappingConfig.Configure();
        return services;
    }

    public static async Task<WebApplication> UseApiServices(this WebApplication app)
    {
        app.UseMetricServer();
        using var scope = app.Services.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        await DatabaseExtensions.SeedAsync(connection, logger);

        app.MapGrpcReflectionService();
        app.MapGrpcService<PromoGrpcService>();
        return app;
    }
}
