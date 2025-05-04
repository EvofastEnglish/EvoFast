using System.Security.Claims;
using EvoFast.Application.ReviewSessions.Commands.UpsertReviewSession;
using EvoFast.Application.ReviewSessions.Queries.GetTotalReviewSessions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class ReviewSessionsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Upsert Review Session")]
    public async Task<ActionResult> UpsertReviewSession([FromBody] UpsertReviewSessionRequest model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new UpsertReviewSessionCommand(model, Guid.Parse(userId));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
    
    [HttpGet("Total")]
    [EndpointSummary("Get Total Review Sessions")]
    public async Task<ActionResult> GetTotalReviewSession()
    {
        var command = new GetTotalReviewSessionQuery();
        var result = await sender.Send(command);
        return Ok(result);
    }
}