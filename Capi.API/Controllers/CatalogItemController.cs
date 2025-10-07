using Capi.Application.Commands.CatalogItemCommands;
using Capi.Application.Queries.CatalogItemsQueries;
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

    [HttpPost]
    [ProducesResponseType(typeof(CreateCatalogItemResult), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateCatalogItemResult>> CreateCatalogItem
        ([FromBody] CreateCatalogItemCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateCatalogItemResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateCatalogItemResult>> UpdateCatalogItem
        ([FromBody] UpdateCatalogItemCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeleteCatalogItemResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteCatalogItemResult>> DeleteCatalogItem(Guid id)
    {
        var result = await Mediator.Send(new DeleteCatalogItemCommand(id));
        return Ok(result);
    }
}
