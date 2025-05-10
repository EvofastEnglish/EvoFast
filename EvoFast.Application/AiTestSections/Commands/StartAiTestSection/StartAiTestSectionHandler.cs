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
            .Include(ait => ait.AiTest)
            .ThenInclude(ai => ai.AiTestResults)
            .FirstOrDefault(a => a.Id == command.StartAiTestSectionRequest.AiTestSectionId);
        
        if (aiTestSection == null)
        {
            throw new NotFoundException("AiTestSection", command.StartAiTestSectionRequest.AiTestSectionId);
        }
        
        var aiTestSectionResult = dbContext.AiTestSectionResults
            .FirstOrDefault(a => a.AiTestSectionId == aiTestSection.Id && a.AiTestResultId == aiTestSection.AiTest.AiTestResults.First().Id);

        if (aiTestSectionResult == null)
        {
            var evaluation = await client.GetResponseAsync(
            [
                new ChatMessage(ChatRole.System, aiTestSection.ChatPrompt),
            ], cancellationToken: cancellationToken);
        
            aiTestSectionResult = new AiTestSectionResult
            {
                AiTestSectionId = aiTestSection.Id,
                AiTestResultId = aiTestSection.AiTest.AiTestResults.First().Id,
                Evaluation = evaluation.Text
            };
            dbContext.AiTestSectionResults.Add(aiTestSectionResult);
            await dbContext.SaveChangesAsync(cancellationToken);   
        }
        var aiTestSectionResultDto = aiTestSectionResult.Adapt<AiTestSectionResultDto>();
        return new StartAiTestSectionResult(aiTestSectionResultDto);    
    }
}