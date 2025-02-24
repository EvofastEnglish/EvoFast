using EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttemp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class QuestionAttemptsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create QuestionAttempt")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateWordSetAttempt([FromBody] CreateQuestionAttemptRequest model)
    {
        var command = new CreateQuestionAttemptCommand(model);
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