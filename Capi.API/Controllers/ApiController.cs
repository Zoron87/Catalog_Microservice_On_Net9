using Asp.Versioning;

namespace Capi.API.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiController : ControllerBase
{
    private IMediator? mediator;

    protected IMediator Mediator =>
        mediator ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("Служба IMediator недоступа");
}
