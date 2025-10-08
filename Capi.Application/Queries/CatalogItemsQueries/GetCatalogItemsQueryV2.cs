using Capi.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Capi.Application.Queries.CatalogItemsQueries;

public record GetCatalogItemsQueryV2(int PageIndex, int PageSize) : IRequest<GetCatalogItemsResultV2>;