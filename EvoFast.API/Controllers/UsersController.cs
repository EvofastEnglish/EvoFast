using System.Security.Claims;
using BuildingBlocks.Pagination;
using EvoFast.Application.QuestionAttempts.Commands.BookmarkQuestionAttempt;
using EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttempt;
using EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttempts;
using EvoFast.Application.QuestionAttempts.Queries.GetQuestionAttemptsByWordSet;
using EvoFast.Application.Users.Commands.UpdateFcmToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPut("FcmToken")]
    [EndpointSummary("Update FcmToken")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> BookmarkQuestionAttempt([FromBody] UpdateFcmTokenRequest model)
    { 
        var command = new UpdateFcmTokenCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}