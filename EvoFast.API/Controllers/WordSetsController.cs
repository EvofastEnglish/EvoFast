using BuildingBlocks.Pagination;
using EvoFast.Application.Users.Commands.CreateUser;
using EvoFast.Application.WordSets.Commands.CreateWordSet;
using EvoFast.Application.WordSets.Commands.DeleteWordSet;
using EvoFast.Application.WordSets.Commands.UpdateWordSet;
using EvoFast.Application.WordSets.Queries.GetWordSets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize("ClientIdPolicy")]
public class WordSetsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create WordSet")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateWordSet([FromBody] CreateWordSetRequest model)
    {
        var command = new CreateWordSetCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPut]
    [EndpointSummary("Update WordSet")]
    public async Task<ActionResult> UpdateWordSet([FromBody] UpdateWordSetRequest model)
    {
        var command = new UpdateWordSetCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    [EndpointSummary("Get WordSets w/ Pagination")]
    public async Task<ActionResult> GetWordSets([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetWordSetsQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{wordSetId}")]
    [EndpointSummary("Delete WordSet")]
    public async Task<ActionResult> DeleteWordSet(Guid wordSetId)
    {
        var command = new DeleteWordSetCommand(wordSetId);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost("User")]
    // [EndpointSummary("Delete WordSet")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest model)
    {
        var command = new CreateUserCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}