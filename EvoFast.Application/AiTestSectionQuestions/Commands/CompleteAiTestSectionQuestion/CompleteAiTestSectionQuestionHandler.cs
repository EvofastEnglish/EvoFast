using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Application.Mapper;
using EvoFast.Application.Services;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using System.Text.Json;

namespace EvoFast.Application.AiTestSectionQuestions.Commands.CompleteAiTestSectionQuestion;

public class CompleteAiTestSectionQuestionHandler(
    IWhisperService  whisperService,
    IApplicationDbContext dbContext,
    IChatClient client)
    : ICommandHandler<CompleteAiTestSectionQuestionCommand, CompleteAiTestSectionQuestionResult>
{
    public async Task<CompleteAiTestSectionQuestionResult> Handle(CompleteAiTestSectionQuestionCommand command, CancellationToken cancellationToken)
    {
        var section = dbContext.AiTestSections
            .FirstOrDefault(t => t.Id == command.CompleteAiTestSectionQuestionRequest.AiTestSectionId);
        if (section == null)
        {
            throw new NotFoundException("AiTestSection", command.CompleteAiTestSectionQuestionRequest.AiTestSectionId);
        }

        var question = dbContext.AiTestSectionQuestions
            .FirstOrDefault(q =>
                q.Id == command.CompleteAiTestSectionQuestionRequest.QuestionId &&
                q.AiTestSectionId == command.CompleteAiTestSectionQuestionRequest.AiTestSectionId);

        if (question == null)
        {
            throw new NotFoundException("Question", command.CompleteAiTestSectionQuestionRequest.QuestionId);
        }

        var session = dbContext.AiTestSessions
            .Include(_ => _.AiTestChatMessages)
            .FirstOrDefault(s =>
                s.Id == command.CompleteAiTestSectionQuestionRequest.AiTestSessionId && s.AiTestId == section.AiTestId);

        if (session == null)
        {
            throw new NotFoundException("AiTestSession", command.CompleteAiTestSectionQuestionRequest.AiTestSectionId);
        }
        
        var chatMessages = session.AiTestChatMessages
            .OrderBy(m => m.CreatedAt)
            .Select(m => new ChatMessage(ChatRoleMapper.MapFromDbRole(m.Role), m.Content))
            .ToList();

        var questPrompt = section.ChatPrompt.Replace("{{QUESTION}}", question.Title);
        chatMessages.Add(new ChatMessage(ChatRole.User, questPrompt));
        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.User.Value,
            questPrompt));
        
        var evaluation = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);
        var evaluationString = JsonSerializer.Serialize(evaluation);
        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.Assistant.Value,
            evaluation.Text, null, evaluationString));

        #region Audio
        
        var transcribeAudio = await whisperService.TranscribeAsync(command.CompleteAiTestSectionQuestionRequest.AudioFile, command.CompleteAiTestSectionQuestionRequest.Language);
        chatMessages.Add(new ChatMessage(ChatRole.User, transcribeAudio));
        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.User.Value,
            transcribeAudio,  transcribeAudio));
        
        var evaluationAudio = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);
        var evaluationAudioString = JsonSerializer.Serialize(evaluationAudio);
        dbContext.AiTestChatMessages.Add(new AiTestChatMessage(session.Id, ChatRole.Assistant.Value,
            evaluationAudio.Text,  transcribeAudio, evaluationAudioString));
        
        #endregion

        await dbContext.SaveChangesAsync(cancellationToken);
        var messageDtos = session.AiTestChatMessages.Adapt<List<AiTestChatMessageDto>>();
        return new CompleteAiTestSectionQuestionResult(messageDtos);      
    }
}