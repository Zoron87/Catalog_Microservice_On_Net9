using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

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
