using Capi.Application.Queries.CatalogItemsQueries;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Repositories;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemsQueryHandlerV2(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsQueryV2, GetCatalogItemsResultV2>
{
    public async Task<GetCatalogItemsResultV2> Handle(GetCatalogItemsQueryV2 query, CancellationToken ct)
    {
        var pagination = await catalogItemRepository.GetCatalogItemsAsync(query.Args);

        return new GetCatalogItemsResultV2(pagination);
    }
}