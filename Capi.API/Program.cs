var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

app.Run();
