using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.AiTestSections.Queries.GetAiTestSectionResult;

public class GetAiTestSectionResultHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAiTestSectionResultQuery, GetAiTestSectionResultResult>
{
    public async Task<GetAiTestSectionResultResult> Handle(GetAiTestSectionResultQuery query, CancellationToken cancellationToken)
    {
        var aiTestSectionResult = dbContext.AiTestSectionResults.AsNoTracking().FirstOrDefault(a => a.AiTestSectionId == query.AiTestSectionId);
        if (aiTestSectionResult == null)
        {
            throw new NotFoundException("AiTestSectionResult with: ", query.AiTestSectionId);
        }
        var aiTestSectionResultDto = aiTestSectionResult.Adapt<AiTestSectionResultDto>();
        return new GetAiTestSectionResultResult(aiTestSectionResultDto);    
    }
}