using System.Security.Claims;
using BuildingBlocks.Pagination;
using EvoFast.Application.AiTests.Commands.CompleteAiTest;
using EvoFast.Application.AiTests.Commands.StartAiTest;
using EvoFast.Application.AiTests.Queries.GetAiTests;
using EvoFast.Application.AiTests.Queries.GetChatMessageBySessionId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize("ClientIdPolicy")]
public class AiTestsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get AiTests w/ Pagination")]
    public async Task<ActionResult> GetAiTests([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetAiTestsQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost("Start")]
    [EndpointSummary("Start AiTest")]
    public async Task<ActionResult> StartAiTest([FromBody] StartAiTestRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new StartAiTestCommand(request, Guid.Parse(userId));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
    
    [HttpPost("Complete/Session")]
    [EndpointSummary("Complete AiTest")]
    public async Task<ActionResult> CompleteAiTest([FromBody] CompleteAiTestRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new CompleteAiTestCommand(request, Guid.Parse(userId));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
    
    [HttpGet("Session/{sessionId}/ChatMessage")]
    [EndpointSummary("Get ChatMessage by SessionId")]
    public async Task<ActionResult> GetChatMessageBySessionId(Guid sessionId)
    {
        var command = new GetChatMessageBySessionIdQuery(sessionId);
        var result = await sender.Send(command);
        return Ok(result);
    }
}