using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;

namespace EvoFast.Application.AiTests.Queries.GetAiTestResult;

public class GetAiTestResultHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAiTestResultQuery, GetAiTestResultResult>
{
    public async Task<GetAiTestResultResult> Handle(GetAiTestResultQuery query, CancellationToken cancellationToken)
    {
        var aiTestResult = dbContext.AiTestResults
            .AsNoTracking()
            .Include(atr => atr.AiTestSectionResults)
            .ThenInclude(atsr => atsr.AiTestSectionQuestionResults.OrderBy(r => r.CreatedAt))            
            .FirstOrDefault(a => a.AiTestId == query.AiTestId);
        
        if (aiTestResult == null)
        {
            throw new NotFoundException("AiTestResult with: ", query.AiTestId);
        }
        var chatMessages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, aiTestResult.ChatPrompt),
            new ChatMessage(ChatRole.Assistant, aiTestResult.Evaluation),
        };
        BuildPreviousChatContext(aiTestResult.AiTestSectionResults, chatMessages);
        return new GetAiTestResultResult(chatMessages);
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