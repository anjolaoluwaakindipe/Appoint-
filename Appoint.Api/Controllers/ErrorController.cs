using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Appoint.Api.Controllers;

[ApiController]
public class ErrorControllers : ControllerBase
{
    private readonly ILogger _logger;

    public ErrorControllers(ILogger<ErrorControllers> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if(exception != null){
            _logger.LogError(exception: exception, message: "System error occured");
        }

        return Problem();
    }
}