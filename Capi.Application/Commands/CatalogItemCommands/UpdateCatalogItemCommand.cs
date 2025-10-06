using Capi.Application.Models;
using Capi.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Capi.Application.Commands.CatalogItemCommands;

public record UpdateCatalogItemCommand(UpdateCatalogItemDTO model) : IRequest<UpdateCatalogItemResult>;