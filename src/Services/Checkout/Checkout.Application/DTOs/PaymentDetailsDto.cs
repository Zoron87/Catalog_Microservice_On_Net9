namespace Checkout.Application.DTOs;

public record PaymentDetailsDto(string CardName, string MAskedCardNumber, string Expiration);
