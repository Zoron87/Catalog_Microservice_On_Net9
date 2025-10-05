using Capi.Domain.Entities;

namespace Capi.Application.Models;

public record CreateCatalogItemDTO(
    string? Title,
    string? ShortDesctiption,
    string? FullDescription,
    string? ImageUrl,
    Brand? Brand,
    Category? Category,
    decimal Price);