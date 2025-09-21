using Capi.Application.Queries.BrandQueries;
using Capi.Application.Responses.BrandResponses;
using Capi.Domain.Entities;
using Capi.Domain.Repositories;
using MediatR;

namespace Capi.Application.Handlers.BrandHandlers;

public class GetBrandsQueryHandler(IBrandRepository brandRepository)
    : IRequestHandler<GetBrandsQuery, GetBrandsResult>
{
    public async Task<GetBrandsResult> Handle(GetBrandsQuery query, CancellationToken ct)
    {
        IEnumerable<Brand> brandList = await brandRepository.GetAllBrandsAsync(ct);
        GetBrandsResult result = new(brandList);
        return result;
    }
}
