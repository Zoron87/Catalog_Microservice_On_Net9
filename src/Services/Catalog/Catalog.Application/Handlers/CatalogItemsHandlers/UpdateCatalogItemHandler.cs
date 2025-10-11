using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using Mapster;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

public class UpdateCatalogItemHandler(ICatalogItemRepository catalogItemRepository) : IRequestHandler<UpdateCatalogItemCommand, UpdateCatalogItemResult>
{
    public async Task<UpdateCatalogItemResult> Handle(UpdateCatalogItemCommand command, CancellationToken ct)
    {
        var existingItem = await catalogItemRepository.GetCatalogItemAsync(command.model.Id);

        if (existingItem is null)
            return new UpdateCatalogItemResult(false);

        command.model.Adapt(existingItem);
        var isSuccess = await catalogItemRepository.UpdateCatalogItemAsync(existingItem, ct);
        return new UpdateCatalogItemResult(isSuccess);
    }
}
