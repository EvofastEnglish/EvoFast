using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;

namespace EvoFast.Application.AiTestSections.Commands.StartAiTestSection;

public class StartAiTestSectionHandler(
    IApplicationDbContext dbContext,
    IChatClient client)
    : ICommandHandler<StartAiTestSectionCommand, StartAiTestSectionResult>
{
    public async Task<StartAiTestSectionResult> Handle(StartAiTestSectionCommand command,
        CancellationToken cancellationToken)
    {
        var section = dbContext.AiTestSections
            .FirstOrDefault(t => t.Id == command.StartAiTestSectionRequest.AiTestSectionId);
        if (section == null)
        {
            throw new NotFoundException("AiTestSection", command.StartAiTestSectionRequest.AiTestSectionId);
        }

        var question = dbContext.AiTestSectionQuestions
            .FirstOrDefault(q =>
                q.Id == command.StartAiTestSectionRequest.QuestionId &&
                q.AiTestSectionId == command.StartAiTestSectionRequest.AiTestSectionId);

        if (question == null)
        {
            throw new NotFoundException("Question", command.StartAiTestSectionRequest.QuestionId);
        }

        var session = dbContext.AiTestSessions
            .Include(_ => _.AiTestChatMessages)
            .FirstOrDefault(s =>
                s.Id == command.StartAiTestSectionRequest.AiTestSessionId && s.AiTestId == section.AiTestId);

        if (session == null)
        {
            throw new NotFoundException("AiTestSession", command.StartAiTestSectionRequest.AiTestSectionId);
        }
        
        var chatMessages = session.AiTestChatMessages
            .OrderBy(m => m.CreatedAt)
            .Select(m => new ChatMessage(MapRole(m.Role), m.Content))
            .ToList();

        var questPrompt = section.ChatPrompt.Replace("{{QUESTION}}", question.Title);
        chatMessages.Add(new ChatMessage(ChatRole.User, questPrompt));
        
        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.User.Value,
            questPrompt));
        var evaluation = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);

        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.Assistant.Value,
            evaluation.Text));
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var messageDtos = session.AiTestChatMessages.Adapt<List<AiTestChatMessageDto>>();
        return new StartAiTestSectionResult(messageDtos);
    }

    static ChatRole MapRole(string dbRole)
    {
        return dbRole?.ToLower() switch
        {
            "user" => ChatRole.User,
            "assistant" => ChatRole.Assistant,
            "system" => ChatRole.System,
            _ => ChatRole.User
        };
    }
}