using Carter;
using Checkout.Application.Orders.Queries.GerOrdersByAccountName;
using Checkout.Domain.Orders;
using Common.Kernel.CQRS.Queries;
using Mapster;
using MediatR;

namespace Checkout.API.Orders.GetOrdersByAccountName;

public record GetOrdersByAccountNameRequest(string AccountName);

public record GetOrdersByAccountNameResponse(IEnumerable<Order> Orders);

public class GetOrdersByAccountNameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] GetOrdersByAccountNameRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetOrdersByAccountNameQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetOrdersByAccountNameResponse>();
            return Results.Ok(response);
        });
    }
}
