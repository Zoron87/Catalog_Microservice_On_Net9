using Capi.Application.Responses.CategoryResponses;
using MediatR;

namespace Capi.Application.Queries.CategoryQueries;

public record GetCategoriesQuery : IRequest<GetCategoriesResult>;
