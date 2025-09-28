using Capi.Application.Queries.CategoryQueries;
using Capi.Application.Responses.CategoryResponses;
using System.Net;

namespace Capi.API.Controllers;

public class CategoriesController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCategoriesResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCategoriesResult>> GetCategory()
    {
        var result = await Mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }
}
