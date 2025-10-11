namespace Catalog.Domain.Specifications;

public record QueryArgs(
    int PageIndex = 1,
    int PageSize = 5,
    Guid? BrandId = null,
    Guid? CategoryId = null,
    string? Search = null)
{
    private const int MaxPageSize = 25;
    /// <summary>
    /// Поле и направление сортировкию <br/>
    /// Возможные значения:
    /// - price_asc - по цене по возрастанию
    /// - price_desc - по цене по убыванию
    /// - title_asc - по названию по убыванию
    /// - title_desc - по названию по убыванию
    /// </summary>
    public string? Sort { get; init; }

    public int PageSize { get; init;  } = 
        PageSize > MaxPageSize ? MaxPageSize : PageSize;
}
