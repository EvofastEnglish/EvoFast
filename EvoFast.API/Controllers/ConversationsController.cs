using EvoFast.Application.Conversations.Commands.AddConversation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class ConversationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create Conversation")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateConversation([FromBody] CreateConversationRequest model)
    {
        var command = new CreateConversationCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}