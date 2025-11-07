using Carter;
using Mapster;
using MediatR;
using static Basket.API.ShoppingBasket.Save.SaveCartOperation;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart", async (SaveCartRequest request, ISender sender, CancellationToken ct) =>
        {
            var command = request.Adapt<SaveCartCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<SaveCartResponse>();
            //return Results.Created($"/cart/{response.AccountName}", response);
            return Results.CreatedAtRoute("RetrieveCartEndpoint", new { accountName = response.AccountName }, response);
        })
        .WithName("SaveCartEndpoint")
        .Produces<SaveCartResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Сохранение корзины")
        .WithDescription("Сохраняет корзину пользователя и возвращает имя аккаунта");
    }
}
