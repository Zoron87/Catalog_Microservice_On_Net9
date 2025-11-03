using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Basket.API.ShoppingBasket.Remove.RemoveCartOperation;

namespace Basket.API.ShoppingBasket.Remove;

public class RemoveCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cart", async ([FromBody] RemoveCartRequest request, ISender sender, CancellationToken ct) =>
        {
            var command = request.Adapt<RemoveCartCommand>();
            var result = await sender.Send(command, ct);
            var response = result.Adapt<RemoveCartResponse>();
            return Results.Ok(response);
        })
        .WithName("RemoveCartEndpoint")
        .Produces<RemoveCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Удаление корзины")
        .WithDescription("Удаляем корзину пользователя и возвращает результат");
    }
}
