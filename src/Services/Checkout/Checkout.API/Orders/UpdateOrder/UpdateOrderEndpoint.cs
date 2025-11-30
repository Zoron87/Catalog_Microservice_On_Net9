using Carter;
using Checkout.Application.Orders.Commands.UpdateOrder;
using Checkout.Application.Orders.Commands.UpdateOrder.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.Orders.UpdateOrder;

public record UpdateOrderRequest(UpdateOrderRequestDto requestData);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async ([FromBody] UpdateOrderRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new UpdateOrderCommand(request.requestData));
            var response = new UpdateOrderResponse(result.IsSuccess);

            return result.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
        });
    }
}
