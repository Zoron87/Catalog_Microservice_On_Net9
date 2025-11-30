namespace Checkout.Application.Orders.Commands.UpdateOrder.Dto;

public record UpdateOrderRequestDto(
    string OrderId,
    UpdateOrderDto OrderData
    );
