using Capi.Application.Commands.CatalogItemCommands;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Repositories;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsHandlers;

public record DeleteCatalogItemHandler(ICatalogItemRepository catalogItemRepository) : IRequestHandler<DeleteCatalogItemCommand, DeleteCatalogItemResult>
{
    public async Task<DeleteCatalogItemResult> Handle(DeleteCatalogItemCommand command, CancellationToken ct)
    {
        var existingItem = await catalogItemRepository.GetCatalogItemAsync(command.Id, ct);

        if (existingItem is null)  
            return new DeleteCatalogItemResult(false);

        var isSuccess = await catalogItemRepository.DeleteCatalogItemAsync(command.Id, ct);
        return new DeleteCatalogItemResult(isSuccess);
    }
}
