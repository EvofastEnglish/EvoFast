using EvoFast.Application.WordSetAttempts.Commands.CompleteWordSetAttempt;
using EvoFast.Application.WordSetAttempts.Commands.CreateWordSetAttempt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class WordSetAttemptsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create WordSetAttempt")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateWordSetAttempt([FromBody] CreateWordSetAttemptRequest model)
    {
        var command = new CreateWordSetAttemptCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost("Complete")]
    [EndpointSummary("Complete WordSetAttempt")]
    public async Task<ActionResult> CompleteWordSetAttempt([FromBody] CompleteWordSetAttemptRequest model)
    {
        var command = new CompleteWordSetAttemptCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}