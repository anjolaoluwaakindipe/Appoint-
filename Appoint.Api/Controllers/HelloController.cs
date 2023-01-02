using System.ComponentModel;
using Appoint.Application.Hello.Queries.SendMessage;
using Appoint.Contracts.Hello.Requests;
using Appoint.Contracts.Hello.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appoint.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public HelloController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _mediator = sender;
    }

    [HttpPost]
    [Route("message")]
    public async Task<IActionResult> SendHelloMessage([FromBody] HelloRequest helloRequest)
    {
        var query = _mapper.Map<SendMessageQuery>(helloRequest);
        var message = await _mediator.Send(query);
        return message.Match(message => Ok(_mapper.Map<HelloResponse>(message)),
            errors => Problem(errors[0].Description));
    }
}