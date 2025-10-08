using Capi.Application.Responses.CatalogItemResponses;
using Capi.Domain.Specifications;
using MediatR;

namespace Capi.Application.Queries.CatalogItemsQueries;

public record GetCatalogItemsQueryV2(QueryArgs Args) : IRequest<GetCatalogItemsResultV2>;