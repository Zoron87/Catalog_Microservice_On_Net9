using Catalog.Application.Queries.CatalogItemsQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemsByTitleQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsByTitleQuery, GetCatalogItemsByTitleResult>
{
    public async Task<GetCatalogItemsByTitleResult> Handle( GetCatalogItemsByTitleQuery query, CancellationToken ct)
    {
        var catalogItems = await catalogItemRepository.GetCatalogItemsByTitleAsync(query.Title, ct);
        return new GetCatalogItemsByTitleResult(catalogItems);
    }
}
