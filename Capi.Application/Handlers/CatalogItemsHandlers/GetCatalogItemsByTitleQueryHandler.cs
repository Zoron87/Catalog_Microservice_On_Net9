using Capi.Application.Queries.CatalogItemsQueries;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Repositories;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemsByTitleQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsByTitleQuery, GetCatalogItemsByTitleResult>
{
    public async Task<GetCatalogItemsByTitleResult> Handle(GetCatalogItemsByTitleQuery query, CancellationToken ct)
    {
        var catalogItems = await catalogItemRepository.GetCatalogItemsByTitleAsync(query.Title, ct);
        return new GetCatalogItemsByTitleResult(catalogItems);
    }
}
