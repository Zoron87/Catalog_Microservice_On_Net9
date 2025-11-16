namespace Checkout.Domain.ValueObjects;

public record class OrderItem
(
    string CatalogItemName,
    string Quantity,
    decimal UnitPrice
);
