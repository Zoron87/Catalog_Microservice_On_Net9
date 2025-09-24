using Capi.Application.Queries.BrandQueries;
using Capi.Application.Responses.BrandResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Capi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<GetBrandsResult> GetBrand() =>
        mediator.Send(new GetBrandsQuery());
}
