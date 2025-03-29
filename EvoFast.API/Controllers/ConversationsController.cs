using BuildingBlocks.Pagination;
using EvoFast.Application.Conversations.Commands.AddConversation;
using EvoFast.Application.Conversations.Queries.GetConversations;
using EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class ConversationsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get Conversations w/ Pagination")]
    public async Task<ActionResult> GetQuestionsByWordSet([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetConversationsQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
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