using Capi.Application.Queries.BrandQueries;
using Capi.Application.Responses.BrandResponses;

namespace Capi.API.Controllers;

public class BrandsController : ApiController
{
    [HttpGet]
    public Task<GetBrandsResult> GetBrand() =>
        Mediator.Send(new GetBrandsQuery());
}
