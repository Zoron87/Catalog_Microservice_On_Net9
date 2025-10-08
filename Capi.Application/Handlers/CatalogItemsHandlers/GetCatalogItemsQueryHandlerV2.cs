using Capi.Application.Queries.CatalogItemsQueries;
using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Entities;
using Capi.Domain.Repositories;
using Capi.Domain.Specifications;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsHandlers;

public class GetCatalogItemsQueryHandlerV2(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsQueryV2, GetCatalogItemsResultV2>
{
    public async Task<GetCatalogItemsResultV2> Handle(GetCatalogItemsQueryV2 query, CancellationToken ct)
    {
        var allItems = await catalogItemRepository.GetAllCatalogItemAsync(ct);

        var count = allItems.Count();
        var items = allItems
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        var pagination = new Pagination<CatalogItem>(
            PageIndex: query.PageIndex,
            PageSize: query.PageSize,
            TotalCount: count,
            Items: items);

        return new GetCatalogItemsResultV2(pagination);
    }
}