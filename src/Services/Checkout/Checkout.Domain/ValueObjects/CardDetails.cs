namespace Checkout.Domain.ValueObjects;

public record class CardDetails
(
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv
);
