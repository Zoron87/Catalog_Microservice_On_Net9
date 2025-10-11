using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemsQueries;

public record GetCatalogItemsByTitleQuery(string Title) : IRequest<GetCatalogItemsByTitleResult>;
