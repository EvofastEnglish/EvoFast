using EvoFast.Application.WordSetAttempts.Commands.CreateWordSetAttempt;
using EvoFast.Application.WordSets.Commands.CreateWordSet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize("ClientIdPolicy")]
public class WordSetsAttemptController(ISender sender) : ControllerBase
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
    
    // [HttpGet]
    // [EndpointSummary("Get WordSetsAttempt")]
    // public async Task<ActionResult> GetWordSets([FromQuery] PaginationRequest paginationRequest)
    // {
    //     var command = new GetWordSetsQuery(paginationRequest);
    //     var result = await sender.Send(command);
    //     return Ok(result);
    // }
}