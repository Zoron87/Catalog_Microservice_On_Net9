using Checkout.Application.Orders.Commands.UpdateOrder.Dto;
using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Commands;
using Common.Kernel.Exceptions.Handler;
using Mapster;

namespace Checkout.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(UpdateOrderRequestDto UpdateOrderData) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken ct)
    {
        var currentOrder = await orderRepository.GetByIdAsync(Guid.Parse(command.UpdateOrderData.OrderId));

        if (currentOrder is null)
            throw new NotFoundException(nameof(Order), command.UpdateOrderData.OrderId);

        command.UpdateOrderData.OrderData.Adapt(currentOrder);
        var result = await orderRepository.UpdateAsync(currentOrder); ;

        return new UpdateOrderResult(result);
    }
}
