using Carter;
using Checkout.Application.Orders.Commands.DeleteOrder;
using MediatR;

namespace Checkout.API.Orders.DeleteOrder;

public record DeleteOrderRequest(string OrderId);
public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders", async ([AsParameters] DeleteOrderRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(request.OrderId));
            var response = new DeleteOrderResponse(result.IsSuccess);

            return result.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
        });
    }
}
