using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;
using EvoFast.Application.WordSets.Commands.CreateWordSet;
using EvoFast.Application.WordSets.Commands.DeleteWordSet;
using EvoFast.Application.WordSets.Commands.UpdateWordSet;
using EvoFast.Application.WordSets.Queries.GetWordSets;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WordSetController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] WordSetDto model)
    {
        var command = new CreateWordSetCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateWordSetRequest model)
    {
        var command = new UpdateWordSetCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete(Guid wordSetId)
    {
        var command = new DeleteWordSetCommand(wordSetId);
        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetWordSetsQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
}