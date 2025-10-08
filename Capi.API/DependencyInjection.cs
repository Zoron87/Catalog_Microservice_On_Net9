using Asp.Versioning;
using Capi.Application.Queries.BrandQueries;
using Capi.Application.Queries.CatalogItemsQueries;

namespace Template.Capi.API;
public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; // v1
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Catalog API",
                Version = "v1"
            });
            config.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Catalog API",
                Version = "v2"
            });

            config.EnableAnnotations();
        });

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
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Catalog API V2");
            }
                    
            );
        }

        app.MapGet("/", () => "Hello World!");

        return app;
    }
}