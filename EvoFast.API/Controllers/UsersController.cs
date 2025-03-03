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
    public async Task<ActionResult> BookmarkQuestionAttempt([FromBody] string fcmToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new UpdateFcmTokenCommand(new UpdateFcmTokenRequest(fcmToken, Guid.Parse(userId)));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
}