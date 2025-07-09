using System.Security.Claims;
using EvoFast.Application.QuestionActionLog.Commands.CreateQuestionActionLog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class QuestionActionLogsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> StartAiTest([FromBody] CreateQuestionActionLogRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var command = new CreateQuestionActionLogCommand(request, Guid.Parse(userId));
            var result = await sender.Send(command);
            return Ok(result);
        }
        return BadRequest("User ID is missing in the token");
    }
}