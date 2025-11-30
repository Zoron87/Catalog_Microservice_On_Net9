namespace Checkout.Application.Orders.Commands.UpdateOrder.Dto;

public record UpdateCardDetailsDto(
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv
    );
