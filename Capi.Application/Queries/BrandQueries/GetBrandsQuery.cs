using Capi.Application.Responses.BrandResponses;
using MediatR;

namespace Capi.Application.Queries.BrandQueries;

public record GetBrandsQuery : IRequest<GetBrandsResult>;
