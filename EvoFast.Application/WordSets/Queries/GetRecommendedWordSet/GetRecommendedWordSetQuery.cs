using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Queries.GetRecommendedWordSet;

public record GetRecommendedWordSetQuery(Guid UserId) : IQuery<GetRecommendedWordSetResult>;

public record GetRecommendedWordSetResult(WordSetDto? WordSet);