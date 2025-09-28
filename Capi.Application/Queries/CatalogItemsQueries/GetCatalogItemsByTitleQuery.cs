using Capi.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Capi.Application.Queries.CatalogItemsQueries;

public record GetCatalogItemsByTitleQuery(string Title) : IRequest<GetCatalogItemsByTitleResult>;
