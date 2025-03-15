using BuildingBlocks.Pagination;
using EvoFast.Application.Categories.Command.AssignWordSet;
using EvoFast.Application.Categories.Queries.GetCategory;
using EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get Categories w/ Pagination")]
    public async Task<ActionResult> GetCategory([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetCategoryQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost]
    [EndpointSummary("Assign WordSet To Category")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateWordSetAttempt([FromBody] AssignWordSetRequest model)
    {
        var command = new AssignWordSetCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}