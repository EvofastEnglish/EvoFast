using BuildingBlocks.Pagination;
using EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttemp;
using EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttempts;
using EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;
using EvoFast.Application.WordSets.Queries.GetWordSets;
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
    
    [HttpGet("{userId}")]
    [EndpointSummary("Get QuestionAttempts By User")]
    public async Task<ActionResult> GetWordSets([FromQuery] PaginationRequest paginationRequest, Guid userId)
    {
        var command = new GetQuestionAttemptsQuery(paginationRequest, userId);
        var result = await sender.Send(command);
        return Ok(result);
    }
}