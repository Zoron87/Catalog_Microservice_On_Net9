using Capi.Application.Commands.CatalogItemCommands;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Repositories;
using Mapster;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsHandlers;

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
