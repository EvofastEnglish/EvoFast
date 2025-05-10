using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.Extensions.AI;

namespace EvoFast.Application.AiTests.Commands.StartAiTest;

public class StartAiTestHandler(
    IApplicationDbContext dbContext,
    IChatClient client)
    : ICommandHandler<StartAiTestCommand, StartAiTestResult>
{
    public async Task<StartAiTestResult> Handle(StartAiTestCommand command, CancellationToken cancellationToken)
    {
        var aiTest = dbContext.AiTests
            .FirstOrDefault(a => a.Id == command.StartAiTestRequest.AiTestId);
        if (aiTest == null)
        {
            throw new NotFoundException("AiTest", command.StartAiTestRequest.AiTestId);
        }

        var aiTestResult = dbContext.AiTestResults
            .FirstOrDefault(a => a.AiTestId == aiTest.Id);
        
        if (aiTestResult == null)
        {
            var evaluation = await client.GetResponseAsync(
            [
                new ChatMessage(ChatRole.System, aiTest.ChatPromptStart),
            ], cancellationToken: cancellationToken);
        
            aiTestResult = new AiTestResult
            {
                AiTestId = aiTest.Id,
                UserId = command.UserId,
                Evaluation = evaluation.Text
            };
            dbContext.AiTestResults.Add(aiTestResult);
            await dbContext.SaveChangesAsync(cancellationToken);   
        }
        var aiTestResultDto = aiTestResult.Adapt<AiTestResultDto>();
        return new StartAiTestResult(aiTestResultDto);
    }
}