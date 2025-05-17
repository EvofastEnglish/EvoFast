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
        var section = dbContext.AiTestSections
            .Include(ats => ats.AiTestSectionQuestions.Where(q => q.Id == command.StartAiTestSectionRequest.QuestionId))
            .Include(ats => ats.AiTest)
            .ThenInclude(at => at.AiTestResults)
            .ThenInclude(atr => atr.AiTestSectionResults)
            .ThenInclude(atsr => atsr.AiTestSectionQuestionResults.OrderBy(r => r.CreatedAt))
            .FirstOrDefault(a => a.Id == command.StartAiTestSectionRequest.AiTestSectionId);
        
        if (section == null)
        {
            throw new NotFoundException("AiTestSection", command.StartAiTestSectionRequest.AiTestSectionId);
        }
        
        var question = section.AiTestSectionQuestions.FirstOrDefault(q => q.Id == command.StartAiTestSectionRequest.QuestionId);
        if (question == null)
        {
            throw new NotFoundException("Question", command.StartAiTestSectionRequest.QuestionId);
        }
        
        var sectionResult = dbContext.AiTestSectionResults
            .FirstOrDefault(a => a.AiTestSectionId == section.Id && a.AiTestResultId == section.AiTest.AiTestResults.First().Id);

        if (sectionResult == null)
        {
            var aiTestResult = section.AiTest.AiTestResults.First();

            var chatMessages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, aiTestResult.ChatPrompt),
                new ChatMessage(ChatRole.Assistant, aiTestResult.Evaluation),
            };
        
            var previousAiTestSectionResults = aiTestResult.AiTestSectionResults.Where(ats => ats.SectionOrder < section.SectionOrder);
        
            BuildPreviousChatContext(previousAiTestSectionResults, chatMessages);

            chatMessages.Add(new ChatMessage(ChatRole.User, section.ChatPrompt));
        
            var evaluation = await client.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);
            
            sectionResult = new AiTestSectionResult
            {
                AiTestSectionId = section.Id,
                AiTestResultId = aiTestResult.Id,
                Evaluation = evaluation.Text,
                ChatPrompt = section.ChatPrompt.Replace("{{QUESTION}}", question.Title),
                SectionOrder = section.SectionOrder,
                CreatedAt = DateTime.UtcNow,
            };
            dbContext.AiTestSectionResults.Add(sectionResult);
            await dbContext.SaveChangesAsync(cancellationToken);   
        }
        var aiTestSectionResultDto = sectionResult.Adapt<AiTestSectionResultDto>();
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