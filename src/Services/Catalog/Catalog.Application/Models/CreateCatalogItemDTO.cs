using Catalog.Domain.Entities;

namespace Catalog.Application.Models;

public record CreateCatalogItemDTO(
    string? Title,
    string? ShortDescription,
    string? FullDescription,
    string? ImageUrl,
    Brand? Brand,
    Category? Category,
    decimal Price);