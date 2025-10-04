using App.Application.Common.Concretes.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected readonly IMediator _mediator;
    public ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        return result.Status switch
        {
            HttpStatusCode.NoContent => NoContent(),
            _ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
        };
    }

    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        return result.Status switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.Created => Created(result.UrlAsCreated, result),
            _ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
        };
    }
}
