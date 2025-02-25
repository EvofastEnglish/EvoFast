using BuildingBlocks.Pagination;
using EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class QuestionsController(ISender sender) : ControllerBase
{
    [HttpGet("WordSet/{wordSetId}")]
    [EndpointSummary("Get Questions by WordSet w/ Pagination")]
    public async Task<ActionResult> GetQuestionsByWordSet([FromQuery] PaginationRequest paginationRequest, Guid wordSetId)
    {
        var command = new GetQuestionsByWordSetQuery(paginationRequest, wordSetId);
        var result = await sender.Send(command);
        return Ok(result);
    }
}