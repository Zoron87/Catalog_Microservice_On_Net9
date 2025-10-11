using Catalog.Application.Models;
using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Commands.CatalogItemCommands;

public record CreateCatalogItemCommand(CreateCatalogItemDTO model) : IRequest<CreateCatalogItemResult>;