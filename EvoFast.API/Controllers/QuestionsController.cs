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
    [HttpGet("{wordSetId}")]
    [EndpointSummary("Get Questions by WordSet")]
    public async Task<ActionResult> GetQuestionsByWordSet(Guid wordSetId)
    {
        var command = new GetQuestionsByWordSetQuery(wordSetId);
        var result = await sender.Send(command);
        return Ok(result);
    }
}