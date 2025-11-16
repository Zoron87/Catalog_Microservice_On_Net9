namespace Checkout.Domain.ValueObjects;

public record class Address
(
    string Street,
    string City,
    string Region,
    string PostalCode
);
