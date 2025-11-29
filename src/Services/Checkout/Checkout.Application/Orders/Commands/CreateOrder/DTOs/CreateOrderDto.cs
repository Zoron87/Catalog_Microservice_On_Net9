using Checkout.Application.DTOs;
using Checkout.Domain.Enums;

namespace Checkout.Application.Orders.Commands.CreateOrder.DTOs;

public record CreateOrderDto(
                             string AccountName, 
                             ContactDto ContactInfo, 
                             AddressDto DeliveryAddress, 
                             PaymentMethod PaymentMethod,
                             CreateCardDetailsDto? CardDetails,
                             decimal TotalPrice,
                             IEnumerable<OrderItemDto> Items
                            );
