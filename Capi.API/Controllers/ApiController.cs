namespace Capi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private IMediator? mediator;

    protected IMediator Mediator =>
        mediator ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("Служба IMediator недоступа");
}
