using System.Security.Claims;
using BuildingBlocks.Pagination;
using EvoFast.Application.ReviewSessions.Commands.CreateReviewSession;
using EvoFast.Application.ReviewSessions.Commands.DeleteRevewSession;
using EvoFast.Application.ReviewSessions.Commands.UpdateConfidenceReviewSession;
using EvoFast.Application.ReviewSessions.Commands.UpsertReviewSession;
using EvoFast.Application.ReviewSessions.Queries.GetReviewSessions;
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
    [EndpointSummary("Create Review Session")]
    public async Task<ActionResult> CreateReviewSession([FromBody] CreateReviewSessionRequest model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new CreateReviewSessionCommand(model, Guid.Parse(userId));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
    
    [HttpPut("Upsert")]
    [EndpointSummary("Upsert Review Session")]
    public async Task<ActionResult> UpsertReviewSession([FromBody] UpsertReviewSessionRequest model)
    {
        var command = new UpsertReviewSessionCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPut("Confidence")]
    [EndpointSummary("Update Confidence Review Session")]
    public async Task<ActionResult> UpdateConfidenceReviewSession([FromBody] UpdateConfidenceReviewSessionRequest model)
    {
        var command = new UpdateConfidenceReviewSessionCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpGet("Total")]
    [EndpointSummary("Get Total Review Sessions")]
    public async Task<ActionResult> GetTotalReviewSession()
    {
        var command = new GetTotalReviewSessionQuery();
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    [EndpointSummary("Get Review Sessions w/ Pagination")]
    public async Task<ActionResult> GetReviewSessions([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetReviewSessionsQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{Id}")]
    [EndpointSummary("Delete Review Session")]
    public async Task<ActionResult> DeleteAnswer(Guid id)
    {
        var command = new DeleteReviewSessionCommand(id);
        var result = await sender.Send(command);
        return Ok(result);
    }
}