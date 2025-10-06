using Capi.Domain.Entities;

namespace Capi.Application.Models;

public record UpdateCatalogItemDTO(
    Guid Id,
    string? Title,
    string? ShortDescription,
    string? FullDescription,
    string? ImageUrl,
    Brand? Brand,
    Category? Category,
    decimal Price);