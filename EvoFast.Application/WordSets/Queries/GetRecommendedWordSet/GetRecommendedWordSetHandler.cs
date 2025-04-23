using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.WordSets.Queries.GetRecommendedWordSet;

public class GetRecommendedWordSetHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetRecommendedWordSetQuery, GetRecommendedWordSetResult>
{
    public async Task<GetRecommendedWordSetResult> Handle(GetRecommendedWordSetQuery request, CancellationToken cancellationToken)
    {
        var lastAttempt = dbContext.WordSetAttempts
            .Where(w => w.CompletedAt != null)
            .Include(w => w.WordSet)
            .OrderByDescending(w => w.CompletedAt)
            .FirstOrDefault();

        WordSet? recommendedSet;

        if (lastAttempt == null)
        {
            recommendedSet = dbContext.WordSets
                .OrderBy(w => w.NumberId)
                .FirstOrDefault();
        }
        else
        {
            recommendedSet = dbContext.WordSets
                .Where(w => w.NumberId > lastAttempt.WordSet.NumberId)
                .OrderBy(w => w.NumberId)
                .FirstOrDefault();
        }
        var wordSetDto = recommendedSet.Adapt<WordSetDto>();
        return new GetRecommendedWordSetResult(wordSetDto);
    }
}