using Capi.Application.Commands.CatalogItemCommands;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Entities;
using Capi.Domain.Repositories;
using Mapster;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsHandlers;

public class CreateCatalogItemHandler(ICatalogItemRepository catalogItemRepository) : 
    IRequestHandler<CreateCatalogItemCommand, CreateCatalogItemResult>
{
    public async Task<CreateCatalogItemResult> Handle(CreateCatalogItemCommand command, CancellationToken ct)
    {
        var catalogItem = command.Adapt<CatalogItem>();
        catalogItem.Id = Guid.NewGuid();
        await catalogItemRepository.CreateCatalogItemAsync(catalogItem);
        return new CreateCatalogItemResult(catalogItem.Id);
    }
}
