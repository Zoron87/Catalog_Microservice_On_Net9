using Capi.Domain.Entities;
using Capi.Domain.Specifications;

namespace Capi.Application.Responses.CatalogItemResponses;

public record GetCatalogItemsResultV2(Pagination<CatalogItem> CatalogItems);