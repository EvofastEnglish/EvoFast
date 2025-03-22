using BuildingBlocks.Pagination;
using EvoFast.Application.Questions.Commands.AddAnswer;
using EvoFast.Application.Questions.Commands.AssignWordSetCategory;
using EvoFast.Application.Questions.Commands.CreateQuestion;
using EvoFast.Application.Questions.Commands.DeleteAnswer;
using EvoFast.Application.Questions.Commands.UpdateAnswer;
using EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;
using EvoFast.Application.Questions.Queries.GetQuestionsByWordSetCategoryId;
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
    
    [HttpGet("WordSetCategory/{wordSetCategoryId}")]
    [EndpointSummary("Get Questions by WordSetCategory w/ Pagination")]
    public async Task<ActionResult> GetQuestionsByWordSetCategory([FromQuery] PaginationRequest paginationRequest, Guid wordSetCategoryId)
    {
        var command = new GetQuestionsByWordSetCategoryQuery(paginationRequest, wordSetCategoryId);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost]
    [EndpointSummary("Create Question")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionRequest model)
    {
        var command = new CreateQuestionCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost("Answer")]
    [EndpointSummary("Add Answer To Question")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> AddAnswer([FromBody] AddAnswerRequest model)
    {
        var command = new AddAnswerCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPut("Answer")]
    [EndpointSummary("Update Answer Of Question")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> UpdateAnswer([FromBody] UpdateAnswerRequest model)
    {
        var command = new UpdateAnswerCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{QuestionId}/Answer/{AnswerId}")]
    [EndpointSummary("Delete Answer Of Question")]
    public async Task<ActionResult> DeleteAnswer(Guid QuestionId, Guid AnswerId)
    {
        var command = new DeleteAnswerCommand(QuestionId, AnswerId);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPut("AssignWordSetCategory")]
    [EndpointSummary("Assign WordSetCategory to Question")]
    public async Task<ActionResult> AssignWordSetCategory([FromBody] AssignWordSetCategoryRequest model)
    {
        var command = new AssignWordSetCategoryCommand(model);
        var result = await sender.Send(command);
        return Ok(result);
    }
}