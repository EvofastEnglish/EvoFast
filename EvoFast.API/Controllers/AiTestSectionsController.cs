using EvoFast.Application.AiTestSections.Commands.StartAiTestSection;
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
}