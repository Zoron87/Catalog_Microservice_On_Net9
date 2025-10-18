﻿using Basket.API.Infrastructure;
using Basket.API.Models;
using Carter;
using Marten;

namespace Basket.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();

        var assembly = typeof(Program).Assembly;
        var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;

        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssembly(assembly);
        });

        var connectionString = configuration.GetConnectionString("PgConnection")!;
        services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.Schema.For<ShoppingCart>().Identity(x => x.AccountName);
        }).UseLightweightSessions();

        services.AddScoped<ICartRepository, CartRepository>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
