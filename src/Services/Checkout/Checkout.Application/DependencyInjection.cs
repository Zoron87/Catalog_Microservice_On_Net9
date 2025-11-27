using Common.Kernel.Behaviors;
using FluentValidation;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Capi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var licenseKey = configuration.GetSection("MediatR:LicenseKey").Value;
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}
