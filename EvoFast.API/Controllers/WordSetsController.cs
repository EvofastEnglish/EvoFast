using BuildingBlocks.Pagination;
using EvoFast.Application.WordSets.Commands.CreateWordSet;
using EvoFast.Application.WordSets.Commands.DeleteWordSet;
using EvoFast.Application.WordSets.Commands.UpdateWordSet;
using EvoFast.Application.WordSets.Queries.GetRecommendedWordSet;
using EvoFast.Application.WordSets.Queries.GetWordSetCategories;
using EvoFast.Application.WordSets.Queries.GetWordSets;
using EvoFast.Application.WordSets.Queries.GetWordSetsByCategory;
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
    
    [HttpGet("Category/{categoryId}")]
    [EndpointSummary("Get WordSet by Category w/ Pagination")]
    public async Task<ActionResult> GetWordSetsByCategory([FromQuery] PaginationRequest paginationRequest, Guid categoryId)
    {
        var command = new GetWordSetsByCategoryQuery(paginationRequest, categoryId);
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
    
    [HttpGet("{wordSetId}/WordSetCategories")]
    [EndpointSummary("Get WordSetCategories by WordSetId")]
    public async Task<ActionResult> GetWordSetCategories(Guid wordSetId)
    {
        var command = new GetWordSetCategoriesQuery(wordSetId);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpGet("Recommended")]
    [EndpointSummary("Get Recommended WordSet")]
    public async Task<ActionResult> GetRecommendedWordSet()
    {
        var command = new GetRecommendedWordSetQuery();
        var result = await sender.Send(command);
        return Ok(result);
    }
}