using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using Mapster;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

public class CreateCatalogItemHandler(ICatalogItemRepository catalogItemRepository) : 
    IRequestHandler<CreateCatalogItemCommand, CreateCatalogItemResult>
{
    public async Task<CreateCatalogItemResult> Handle(CreateCatalogItemCommand command, CancellationToken ct)
    {
        var catalogItem = command.Adapt<CatalogItem>();
        catalogItem.Id = Guid.NewGuid();
        await catalogItemRepository.CreateCatalogItemAsync(catalogItem);

        //var dto = catalogItem.Adapt<CreateCatalogItemDTO>();
        return new CreateCatalogItemResult(catalogItem.Id);
    }
}
