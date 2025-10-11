using Catalog.Application.Queries.CatalogItemsQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemsQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsQuery, GetCatalogItemsResult>
{
    public async Task<GetCatalogItemsResult> Handle(GetCatalogItemsQuery query, CancellationToken ct)
    {
        var catalogItems = await catalogItemRepository.GetAllCatalogItemAsync(ct);
        return new(catalogItems);
    }
}