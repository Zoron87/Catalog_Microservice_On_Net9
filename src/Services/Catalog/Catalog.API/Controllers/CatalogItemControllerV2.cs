using Asp.Versioning;
using Catalog.Application.Queries.CatalogItemsQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Catalog.API.Controllers;

[ApiVersion("2")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v{version:apiVersion}/CatalogItem")]
public class CatalogItemControllerV2 : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCatalogItemsResultV2), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Tags = new[] { "CatalogItemControllerV2" })]
    public async Task<ActionResult<GetCatalogItemsResultV2>> GetAll(
        [FromQuery] QueryArgs args)
    {
        //var args = new QueryArgs(pageIndex, pageSize);
        var query = new GetCatalogItemsQueryV2(args);
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}