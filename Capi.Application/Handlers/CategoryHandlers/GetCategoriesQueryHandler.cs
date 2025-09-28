using Capi.Application.Queries.CategoryQueries;
using Capi.Application.Responses.CategoryResponses;
using Capi.Domain.Repositories;
using MediatR;

namespace Capi.Application.Handlers.CatalogItemsQueryHandlers;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken ct)
    {
        var categoryList = await categoryRepository.GetAllGategoriesAsync(ct);
        return new(categoryList);
    }
}
