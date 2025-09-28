using Capi.Application.Queries.CatalogItemsQueries;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Repositories;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsQueryHandlers;

public class GetCatalogItemByIdQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemByIdQuery, GetCatalogItemByIdResult>
{
    public async Task<GetCatalogItemByIdResult> Handle(GetCatalogItemByIdQuery query, CancellationToken ct)
    {
        var catalogItem = await catalogItemRepository.GetCatalogItemAsync(query.Id, ct);
        return new GetCatalogItemByIdResult(catalogItem);
    }
}
