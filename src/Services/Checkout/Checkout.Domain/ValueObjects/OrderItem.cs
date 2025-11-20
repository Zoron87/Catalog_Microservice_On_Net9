namespace Checkout.Domain.ValueObjects;

public record class OrderItem
(
    string CatalogItemName,
    int Quantity,
    decimal UnitPrice
);
