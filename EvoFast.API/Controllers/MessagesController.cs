using EvoFast.Application.Messages.Commands.CreateMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class MessagesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create Message")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateMessage([FromBody] CreateMessageRequest model)
    {
        var command = new CreateMessageCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}