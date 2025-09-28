using Capi.Application.Queries.CatalogItemsQueries;
using Capi.Application.Queries.CategoryQueries;
using Capi.Application.Responses.CatalogItemResponses;
using System.Net;

namespace Capi.API.Controllers;

public class CatalogItemController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCatalogItemsResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsResult>> GetAll()
    {
        var result = await Mediator.Send(new GetCatalogItemsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetCatalogItemByIdResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemByIdResult>> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetCatalogItemByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("title/{catalogItemTitle}")]
    [ProducesResponseType(typeof(GetCatalogItemsByTitleResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCatalogItemsByTitleResult>> GetByTitle(string catalogItemTitle)
    {
        var result = await Mediator.Send(new GetCatalogItemsByTitleQuery(catalogItemTitle));
        return Ok(result);
    }
}
