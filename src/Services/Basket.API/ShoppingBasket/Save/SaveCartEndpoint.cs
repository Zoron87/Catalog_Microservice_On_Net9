using Carter;
using Mapster;
using MediatR;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart", async(SaveCartRequest request, ISender sender, CancellationToken ct) =>
        {
            var command = request.Adapt<SaveCartCommand>();
            var result = await sender.Send(command, ct);
            var response = result.Adapt<SaveCartResponse>();
            return Results.CreatedAtRoute($"/cart/{response.AccountName}", response);
        });
    }
}
