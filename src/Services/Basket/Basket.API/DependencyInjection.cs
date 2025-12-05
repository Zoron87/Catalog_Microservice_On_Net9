using Basket.API.Infrastructure;
using Basket.API.Models;
using Carter;
using Common.Kernel.Behaviors;
using Common.Kernel.Exceptions.Handler;
using Common.Logging.Extensions;
using Common.Logging.Middleware;
using Common.Messaging.Extensions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Marten;
using Promotion.Grpc.Protos;

namespace Basket.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();

        var assembly = typeof(Program).Assembly;
        var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;

        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        var connectionString = configuration.GetConnectionString("PgConnection")!;
        services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.Schema.For<ShoppingCart>().Identity(x => x.AccountName);
        }).UseLightweightSessions();

        var redisConnectionString = configuration.GetConnectionString("RedisConnection");
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "Cart";  
        });

        //services.AddScoped<CartRepository>();
        //services.AddScoped<ICartRepository>(provider =>
        //        new RedisCartCacheRepository(provider.GetRequiredService<CartRepository>(),
        //                                     provider.GetRequiredService<IDistributedCache>()
        //                                    )
        //        );
        services.AddScoped<ICartRepository, CartRepository>();
        services.Decorate<ICartRepository, RedisCartCacheRepository>();

        var promotionService = configuration.GetSection("GrpcServices:PromotionService").Value!;    
        services.AddGrpcClient<PromoService.PromoServiceClient>(option =>
        {
            option.Address = new Uri(promotionService);
        });

        services.AddBroker(configuration);

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddCommonLogging(configuration);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseExceptionHandler(option => { });
        app.MapCarter();
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
