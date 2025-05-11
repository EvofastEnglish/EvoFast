using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Application.Services;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;

namespace EvoFast.Application.AiTestSectionQuestions.Commands.CompleteAiTestSectionQuestion;

public class CompleteAiTestSectionQuestionHandler(
    IWhisperService  whisperService,
    IApplicationDbContext dbContext,
    IChatClient client)
    : ICommandHandler<CompleteAiTestSectionQuestionCommand, CompleteAiTestSectionQuestionResult>
{
    public async Task<CompleteAiTestSectionQuestionResult> Handle(CompleteAiTestSectionQuestionCommand command, CancellationToken cancellationToken)
    {
        var aiTestSectionQuestion = dbContext.AiTestSectionQuestions
            .Include(q => q.AiTestSection)
            .ThenInclude(ats => ats.AiTest)
            .ThenInclude(at => at.AiTestResults)
            .ThenInclude(ats => ats.AiTestSectionResults)
            .ThenInclude(atsr => atsr.AiTestSectionQuestionResults.OrderBy(r => r.CreatedAt))
            .FirstOrDefault(a => a.Id == command.CompleteAiTestSectionQuestionRequest.AiTestSectionQuestionId);
        
        if (aiTestSectionQuestion == null)
        {
            throw new NotFoundException("AiTestSectionQuestion", command.CompleteAiTestSectionQuestionRequest.AiTestSectionQuestionId);
        }
        var aiTestSectionQuestionResult = dbContext.AiTestSectionQuestionResults
            .FirstOrDefault(a => a.AiTestSectionQuestionId == aiTestSectionQuestion.Id && a.AiTestSectionResultId == aiTestSectionQuestion.AiTestSection.AiTestSectionResults.First().Id);
        
        if (aiTestSectionQuestionResult == null)
        {
            var aiTestResult = aiTestSectionQuestion.AiTestSection.AiTest.AiTestResults.First();
            var chatMessages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, aiTestResult.ChatPrompt),
                new ChatMessage(ChatRole.Assistant, aiTestResult.Evaluation),
            };
        
            var previousAiTestSectionResults = aiTestResult.AiTestSectionResults.Where(ats => ats.SectionOrder <= aiTestSectionQuestion.AiTestSection.SectionOrder);
        
            BuildPreviousChatContext(previousAiTestSectionResults, chatMessages);
        
            var transcribeAudio = await whisperService.TranscribeAsync(command.CompleteAiTestSectionQuestionRequest.AudioFile, command.CompleteAiTestSectionQuestionRequest.Language);
        
            chatMessages.Add(new ChatMessage(ChatRole.User, transcribeAudio));

            var evaluation = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);
            
            aiTestSectionQuestionResult = new AiTestSectionQuestionResult
            {
                AiTestSectionQuestionId = aiTestSectionQuestion.Id,
                AiTestSectionResultId = aiTestSectionQuestion.AiTestSection.AiTestSectionResults.First().Id,
                Evaluation = evaluation.Text,
                ChatPrompt = aiTestSectionQuestion.Title,
                CreatedAt = DateTime.UtcNow,
            };
            dbContext.AiTestSectionQuestionResults.Add(aiTestSectionQuestionResult);
            await dbContext.SaveChangesAsync(cancellationToken);   
        }
        var aiTestSectionQuestionResultDto = aiTestSectionQuestionResult.Adapt<AiTestSectionQuestionResultDto>();
        return new CompleteAiTestSectionQuestionResult(aiTestSectionQuestionResultDto);      
    }
    
    private List<ChatMessage> BuildPreviousChatContext(IEnumerable<AiTestSectionResult> previousResults, List<ChatMessage> messages)
    {
        foreach (var result in previousResults.OrderBy(r => r.SectionOrder))
        {
            messages.Add(new ChatMessage(ChatRole.User, result.ChatPrompt));
            messages.Add(new ChatMessage(ChatRole.Assistant, result.Evaluation));

            if (result.AiTestSectionQuestionResults != null)
            {   
                foreach (var questionResult in result.AiTestSectionQuestionResults)
                {
                    messages.Add(new ChatMessage(ChatRole.Assistant, questionResult.Evaluation));
                }
            }
        }

        return messages;
    }
}