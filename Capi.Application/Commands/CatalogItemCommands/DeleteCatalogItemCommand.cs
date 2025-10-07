using Capi.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Capi.Application.Commands.CatalogItemCommands;

public record DeleteCatalogItemCommand(Guid Id) : IRequest<DeleteCatalogItemResult>;