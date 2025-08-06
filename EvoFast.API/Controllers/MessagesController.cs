using BuildingBlocks.Pagination;
using EvoFast.Application.Messages.Commands.CreateMessage;
using EvoFast.Application.Messages.Commands.DeleteMessages;
using EvoFast.Application.Messages.Queries.GetMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class MessagesController(ISender sender) : ControllerBase
{
    [HttpGet("{conversationId}")]
    [EndpointSummary("Get Messages w/ Pagination")]
    public async Task<ActionResult> GetMessages([FromQuery] PaginationRequest paginationRequest, Guid conversationId)
    {
        var command = new GetMessagesQuery(paginationRequest, conversationId);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost]
    [EndpointSummary("Create Message")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateMessage([FromBody] CreateMessageRequest model)
    {
        var command = new CreateMessageCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{conversationId}")]
    [EndpointSummary("Delete Messages By ConversationId")]
    public async Task<ActionResult> DeleteMessages(Guid conversationId)
    {
        var command = new DeleteMessagesCommand(conversationId);
        var result = await sender.Send(command);
        return Ok(result);
    }
}