using Capi.Domain.Entities;

namespace Capi.Application.Responses.CategoryResponses;

public record GetCategoriesResult (IEnumerable<Category> Categories);