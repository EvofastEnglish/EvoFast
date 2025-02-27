using System.Security.Claims;
using BuildingBlocks.Pagination;
using EvoFast.Application.QuestionAttempts.Commands.BookmarkQuestionAttempt;
using EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttempt;
using EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttempts;
using EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttemptsByWordSet;
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
    
    [HttpGet]
    [EndpointSummary("Get QuestionAttempts (For User)")]
    public async Task<ActionResult> GetQuestionAttempts([FromQuery] PaginationRequest paginationRequest)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new GetQuestionAttemptsQuery(paginationRequest, Guid.Parse(userId));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
    
    [HttpGet("WordSet/{WordSetId}")]
    [EndpointSummary("Get QuestionAttempts (For User) By WordSet")]
    public async Task<ActionResult> QuestionAttempts([FromQuery] PaginationRequest paginationRequest, Guid WordSetId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new GetQuestionAttemptsByWordSetQuery(paginationRequest, Guid.Parse(userId), WordSetId);
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
    
    [HttpPut("Bookmark")]
    [EndpointSummary("Bookmark QuestionAttempt")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> BookmarkQuestionAttempt([FromBody] BookmarkQuestionAttemptRequest model)
    {
        var command = new BookmarkQuestionAttemptCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}