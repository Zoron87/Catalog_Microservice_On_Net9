using Microsoft.AspNetCore.Components.Forms;
using MySqlConnector;
using Promotion.Grpc.Configuration;
using Promotion.Grpc.Persistance.Extensions;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Persistance.Repositories;
using Promotion.Grpc.Services;
using System.Data;
using System.Threading.Tasks;

namespace Promotion.Grpc;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("MySqlConnection");
        services.AddScoped<IDbConnection>(_ =>
                new MySqlConnection(mySqlConnection));

        services.AddGrpc();
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
        using var scope = app.Services.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        await DatabaseExtensions.SeedAsync(connection, logger);

        app.MapGrpcReflectionService();
        app.MapGrpcService<PromoGrpcService>();
        return app;
    }
}
