using Capi.Application.Queries.BrandQueries;
using Capi.Application.Responses.BrandResponses;
using System.Net;

namespace Capi.API.Controllers;

public class BrandsController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetBrandsResult), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBrandsResult>> GetBrand()
    {
        var result = await Mediator.Send(new GetBrandsQuery());
        return Ok(result);
    }
}
