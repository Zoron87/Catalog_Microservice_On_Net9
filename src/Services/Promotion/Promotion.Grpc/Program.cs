using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();
await app.UseApiServices();

app.Run();
