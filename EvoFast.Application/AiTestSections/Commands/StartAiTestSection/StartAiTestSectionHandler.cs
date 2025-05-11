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
    public async Task<StartAiTestSectionResult> Handle(StartAiTestSectionCommand command, CancellationToken cancellationToken)
    {
        var aiTestSection = dbContext.AiTestSections
            .Include(ats => ats.AiTest).ThenInclude(at => at.AiTestResults)
            .Include(ats => ats.AiTestSectionResults).ThenInclude(atsr => atsr.AiTestSectionQuestionResults.OrderBy(r => r.CreatedAt))
            .FirstOrDefault(a => a.Id == command.StartAiTestSectionRequest.AiTestSectionId);
        
        if (aiTestSection == null)
        {
            throw new NotFoundException("AiTestSection", command.StartAiTestSectionRequest.AiTestSectionId);
        }
        
        var aiTestSectionResult = dbContext.AiTestSectionResults
            .FirstOrDefault(a => a.AiTestSectionId == aiTestSection.Id && a.AiTestResultId == aiTestSection.AiTest.AiTestResults.First().Id);

        if (aiTestSectionResult == null)
        {
            var aiTestResult = aiTestSection.AiTest.AiTestResults.First();

            var chatMessages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, aiTestResult.ChatPrompt),
                new ChatMessage(ChatRole.Assistant, aiTestResult.Evaluation),
            };
        
            var previousAiTestSectionResults = aiTestSection.AiTestSectionResults.Where(ats => ats.SectionOrder < aiTestSection.SectionOrder);
        
            BuildPreviousChatContext(previousAiTestSectionResults, chatMessages);

            chatMessages.Add(new ChatMessage(ChatRole.User, aiTestSection.ChatPrompt));
        
            var evaluation = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);
            
            aiTestSectionResult = new AiTestSectionResult
            {
                AiTestSectionId = aiTestSection.Id,
                AiTestResultId = aiTestResult.Id,
                Evaluation = evaluation.Text,
                ChatPrompt = aiTestSection.ChatPrompt,
                SectionOrder = aiTestSection.SectionOrder,
                CreatedAt = DateTime.UtcNow,
            };
            dbContext.AiTestSectionResults.Add(aiTestSectionResult);
            await dbContext.SaveChangesAsync(cancellationToken);   
        }
        var aiTestSectionResultDto = aiTestSectionResult.Adapt<AiTestSectionResultDto>();
        return new StartAiTestSectionResult(aiTestSectionResultDto);    
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