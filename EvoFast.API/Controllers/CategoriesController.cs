using BuildingBlocks.Pagination;
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
}