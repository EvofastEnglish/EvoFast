using EvoFast.Application.AiTestSectionQuestions.Commands.CompleteAiTestSectionQuestion;
using EvoFast.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ClientIdPolicy")]
public class AiTestSectionQuestionsController(ISender sender, IWhisperService whisperService) : ControllerBase
{
    [HttpPost("Complete")]
    [EndpointSummary("Complete AiTestSectionQuestion")]
    public async Task<ActionResult> CompleteAiTestSectionQuestion([FromForm] CompleteAiTestSectionQuestionRequest request)
    {
        var command = new CompleteAiTestSectionQuestionCommand(request);
        var result = await sender.Send(command);
        return Ok(result);
    }
    
    [HttpPost("Test")]
    [EndpointSummary("Test Audio")]
    public async Task<ActionResult> TestAudio([FromForm] IFormFile audioFile, [FromForm] string language)
    {
        var testAudio = await whisperService.TranscribeAsync(audioFile, language);
        return Ok(testAudio);
    }
}