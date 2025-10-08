namespace Capi.Domain.Specifications;

public record QueryArgs(
    int PageIndex = 1,
    int PageSize = 5,
    Guid? BrandId = null,
    Guid? CategoryId = null,
    string? Sort = null,
    string? Search = null)
{
    private const int MaxPageSize = 25;

    public int PageSize { get; init;  } = 
        PageSize > MaxPageSize ? MaxPageSize : PageSize;
}
