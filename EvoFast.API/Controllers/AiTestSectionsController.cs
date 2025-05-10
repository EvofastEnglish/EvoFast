using EvoFast.Application.AiTestSections.Commands.StartAiTestSection;
using EvoFast.Application.AiTestSections.Queries.GetAiTestSectionResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class AiTestSectionsController(ISender sender) : ControllerBase
{
    [HttpPost("Start")]
    [EndpointSummary("Start AiTestSection")]
    public async Task<ActionResult> StartAiTestSection([FromBody] StartAiTestSectionRequest request)
    {
        var command = new StartAiTestSectionCommand(request);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpGet("Result")]
    [EndpointSummary("Get Result of AiTestSection")]
    public async Task<ActionResult> GetAiTestSectionResult(Guid aiTestSectionId)
    {
        var command = new GetAiTestSectionResultQuery(aiTestSectionId);
        var result = await sender.Send(command);
        return Ok(result);
    }
}