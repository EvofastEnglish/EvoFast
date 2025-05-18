using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Application.Mapper;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;

namespace EvoFast.Application.AiTests.Commands.CompleteAiTest;

public class CompleteAiTestHandler(
    IApplicationDbContext dbContext,
    IChatClient client)
    : ICommandHandler<CompleteAiTestCommand, CompleteAiTestResult>
{
    public async Task<CompleteAiTestResult> Handle(CompleteAiTestCommand command, CancellationToken cancellationToken)
    {
        
        var session = dbContext.AiTestSessions
            .Include(_ => _.AiTest)
            .Include(_ => _.AiTestChatMessages)
            .FirstOrDefault(s =>
                s.Id == command.CompleteAiTestRequest.AiTestSessionId);

        if (session == null)
        {
            throw new NotFoundException("AiTestSession", command.CompleteAiTestRequest.AiTestSessionId);
        }
        
        var chatMessages = session.AiTestChatMessages
            .OrderBy(m => m.CreatedAt)
            .Select(m => new ChatMessage(ChatRoleMapper.MapFromDbRole(m.Role), m.Content))
            .ToList();
        
        chatMessages.Add(new ChatMessage(ChatRole.User, session.AiTest.ChatPromptFinish));
        var evaluation = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);
        
        session.Complete(evaluation.Text);

        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.Assistant.Value,
            evaluation.Text));

        await dbContext.SaveChangesAsync(cancellationToken);
        var sessionDto = session.Adapt<AiTestSessionDto>();
        return new CompleteAiTestResult(sessionDto);      
    }
}