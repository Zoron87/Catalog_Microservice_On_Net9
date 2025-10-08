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

        var brandId = query.Args.BrandId;
        if (brandId is not null)
            allItems.Where(e => e.Brand?.Id == brandId);

        var categoryId = query.Args.CategoryId;
        if (categoryId is not null)
            allItems.Where(e => e.Category?.Id == categoryId);

        var search = query.Args.Search;
        if (!string.IsNullOrWhiteSpace(search))
            allItems.Where(e => e.Title != null && e.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrWhiteSpace(query.Args.Sort))
        {
            allItems = query.Args.Sort.ToLower() switch
            {
                "price_desc" => allItems.OrderByDescending(x => x.Price),
                "price_asc" => allItems.OrderBy(x => x.Price),
                "title_desc" => allItems.OrderByDescending(x => x.Title),
                "title_asc" => allItems.OrderBy(x => x.Title),
            };
        }

        var count = allItems.Count();
        var items = allItems
            .Skip((query.Args.PageIndex - 1) * query.Args.PageSize)
            .Take(query.Args.PageSize)
            .ToList();

        var pagination = new Pagination<CatalogItem>(
            PageIndex: query.Args.PageIndex,
            PageSize: query.Args.PageSize,
            TotalCount: count,
            Items: items);

        return new GetCatalogItemsResultV2(pagination);
    }
}