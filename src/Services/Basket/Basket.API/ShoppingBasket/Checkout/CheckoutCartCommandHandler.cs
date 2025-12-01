using Basket.API.ShoppingBasket.Remove;
using Common.Kernel.CQRS.Commands;
using Common.Kernel.Exceptions.Handler;
using Common.Messaging.DTOs;
using Common.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;
using static Basket.API.ShoppingBasket.Cart.RetrieveCartOperation;
using static Basket.API.ShoppingBasket.Remove.RemoveCartOperation;

namespace Basket.API.ShoppingBasket.Checkout;

public class CheckoutCartCommandHandler (IPublishEndpoint publishEndpoint, ISender sender)
    : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken ct)
    {
        var accountName = command.AccountName;
        var query = new RetrieveCartQuery(accountName);
        var basket = await sender.Send(query, ct);

        if (basket?.Result is null)
            throw new CartNotFoundException(accountName);

        var orderId = Guid.NewGuid();
        var orderEvent = command.Adapt<OrderSubmittedEvent>();
        orderEvent.OrderId = orderId;
        orderEvent.TotalPrice = basket.Result.TotalPrice;

        orderEvent.Items = basket.Result.Items
            .Select(item => item.Adapt<OrderItemEventDto>())
            .ToList();

        await publishEndpoint.Publish(orderEvent, ct);

        var removeCart = new RemoveCartCommand(accountName);
        var result = await sender.Send(removeCart, ct);

        return new CheckoutCartResult(orderId, command.CorrelationId, result.IsSuccess);
    }
}
