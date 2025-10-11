using Catalog.Application.Queries.CatalogItemsQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemByIdQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemByIdQuery, GetCatalogItemByIdResult>
{
    public async Task<GetCatalogItemByIdResult> Handle(GetCatalogItemByIdQuery query, CancellationToken ct)
    {
        var catalogItem = await catalogItemRepository.GetCatalogItemAsync(query.Id, ct);
        return new GetCatalogItemByIdResult(catalogItem);
    }
}
