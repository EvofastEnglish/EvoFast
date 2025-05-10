using BuildingBlocks.Pagination;
using EvoFast.Application.AiTests.Queries.GetAiTests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class AiTestsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get AiTests w/ Pagination")]
    public async Task<ActionResult> GetAiTests([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new GetAiTestsQuery(paginationRequest);
        var result = await sender.Send(command);
        return Ok(result);
    }
}