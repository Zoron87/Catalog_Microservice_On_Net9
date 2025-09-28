using Capi.Domain.Entities;

namespace Capi.Application.Responses.CatalogItemResponses;

public record GetCatalogItemByIdResult(CatalogItem? Result);