using Capi.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Capi.Application.Queries.CatalogItemsQueries;

public record GetCatalogItemByIdQuery(Guid Id) : IRequest<GetCatalogItemByIdResult>;