using Catalog.Application.Queries.CategoryQueries;
using Catalog.Application.Responses.CategoryResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CategoryHandlers;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken ct)
    {
        var categoryList = await categoryRepository.GetAllGategoriesAsync(ct);
        return new(categoryList);
    }
}
