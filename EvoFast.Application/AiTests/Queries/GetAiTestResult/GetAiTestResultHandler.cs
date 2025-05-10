using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.AiTests.Queries.GetAiTestResult;

public class GetAiTestResultHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAiTestResultQuery, GetAiTestResultResult>
{
    public async Task<GetAiTestResultResult> Handle(GetAiTestResultQuery query, CancellationToken cancellationToken)
    {
        var aiTestResult = dbContext.AiTestResults.AsNoTracking().FirstOrDefault(a => a.AiTestId == query.AiTestId);
        if (aiTestResult == null)
        {
            throw new NotFoundException("AiTestResult with: ", query.AiTestId);
        }
        var aiTestReslutDto = aiTestResult.Adapt<AiTestResultDto>();
        return new GetAiTestResultResult(aiTestReslutDto);
    }
}