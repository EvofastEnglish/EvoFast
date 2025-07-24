using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.Extensions.AI;
using System.Text.Json;

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

        var aiTestSession = new AiTestSession(aiTest.Id, command.UserId);
        dbContext.AiTestSessions.Add(aiTestSession);

        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(aiTestSession.Id, ChatRole.System.Value,
            aiTest.ChatPromptStart));

        var evaluation = await client.GetResponseAsync(
        [
            new ChatMessage(ChatRole.System, aiTest.ChatPromptStart),
        ], cancellationToken: cancellationToken);
        var responseString = JsonSerializer.Serialize(evaluation);
        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(aiTestSession.Id, ChatRole.Assistant.Value,
            evaluation.Text, null, responseString));
        
        await dbContext.SaveChangesAsync(cancellationToken);
        var aiTestSessionDto = aiTestSession.Adapt<AiTestSessionDto>();
        return new StartAiTestResult(aiTestSessionDto);
    }
}