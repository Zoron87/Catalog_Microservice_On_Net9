using Capi.Application.Queries.BrandQueries;

namespace Template.Capi.API;
public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var licenceKey = configuration.GetSection("MediatR:LicenseKey").Value;
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = licenceKey;
            cfg.RegisterServicesFromAssemblyContaining<GetBrandsQuery>();
        });

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapControllers();
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/", () => "Hello World!");

        return app;
    }
}