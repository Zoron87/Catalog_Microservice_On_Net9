using Checkout.Application.Orders.Commands.DeleteOrder.Dto;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Commands;
using Common.Kernel.Exceptions.Handler;

namespace Checkout.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(string OrderId) : ICommand<DeleteOrderResultDto>;

public class DeleteOrderCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResultDto>
{
    public async Task<DeleteOrderResultDto> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = Guid.Parse(command.OrderId);
        var order = await orderRepository.GetByIdAsync(orderId);

        if (order is null) 
            throw new NotFoundException(nameof(order), orderId);

        var result = await orderRepository.DeleteAsync(order);
        return new DeleteOrderResultDto(result);    
    }
}
