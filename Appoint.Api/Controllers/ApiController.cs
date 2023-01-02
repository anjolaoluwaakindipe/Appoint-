using System.Net;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Appoint.Api.Controllers;

[ApiController]
public class ApiController: ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if(errors.Count is 0)
        {
            return Problem();
        }

        return Problem(errors[0]);
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}