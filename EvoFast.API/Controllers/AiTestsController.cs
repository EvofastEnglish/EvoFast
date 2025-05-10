using System.Security.Claims;
using BuildingBlocks.Pagination;
using EvoFast.Application.AiTests.Commands.StartAiTest;
using EvoFast.Application.AiTests.Queries.GetAiTestResult;
using EvoFast.Application.AiTests.Queries.GetAiTests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
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
    
    [HttpGet("Result")]
    [EndpointSummary("Get Result of AiTest")]
    public async Task<ActionResult> GetAiTestResult(Guid aiTestId)
    {
        var command = new GetAiTestResultQuery(aiTestId);
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
}