using Common.Messaging.DTOs;

namespace Common.Messaging.Events;

public class OrderSubmittedEvent : BaseIntegrationEvent
{
    public Guid OrderId { get; set; }
    public string AccountName { get; set; } = default!;
    public string TotalPrice { get; set; }

    // Контактная информация
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;

    // Адрес доставки
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string PostalCode { get; set; } = default!;

    // Информация о платеже
    public int PaymentMethod { get; set; }
    public string? CardName{ get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? Cvv { get; set; }

    public List<OrderItemEventDto> items { get; set; } = new();
}
