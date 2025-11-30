namespace Common.Messaging.DTOs;

public class OrderItemEventDto
{
    public string CatalogItemName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
