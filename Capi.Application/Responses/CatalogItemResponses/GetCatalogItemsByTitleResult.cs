using Capi.Domain.Entities;

namespace Capi.Application.Responses.CatalogItemResponses;

public record GetCatalogItemsByTitleResult(IEnumerable<CatalogItem>? Result);