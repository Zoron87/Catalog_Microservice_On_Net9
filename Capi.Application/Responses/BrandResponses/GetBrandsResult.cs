using Capi.Domain.Entities;

namespace Capi.Application.Responses.BrandResponses;

public record GetBrandsResult(IEnumerable<Brand> Brands);