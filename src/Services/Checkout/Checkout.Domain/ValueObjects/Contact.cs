namespace Checkout.Domain.ValueObjects;

public record class Contact
(
    string FirstName,
    string LastName,
    string Email
);
