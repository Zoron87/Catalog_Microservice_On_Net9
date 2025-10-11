using Catalog.Application.Queries.CatalogItemsQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemsQueryHandlerV2(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsQueryV2, GetCatalogItemsResultV2>
{
    public async Task<GetCatalogItemsResultV2> Handle(GetCatalogItemsQueryV2 query, CancellationToken ct)
    {
        var pagination = await catalogItemRepository.GetCatalogItemsAsync(query.Args);

        return new GetCatalogItemsResultV2(pagination);
    }
}