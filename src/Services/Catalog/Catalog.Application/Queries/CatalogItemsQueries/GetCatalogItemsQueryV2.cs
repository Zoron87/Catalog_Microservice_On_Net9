using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Specifications;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemsQueries;

public record GetCatalogItemsQueryV2(QueryArgs Args) : IRequest<GetCatalogItemsResultV2>;