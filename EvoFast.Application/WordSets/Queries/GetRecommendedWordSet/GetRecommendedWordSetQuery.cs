using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Queries.GetRecommendedWordSet;

public record GetRecommendedWordSetQuery : IQuery<GetRecommendedWordSetResult>;

public record GetRecommendedWordSetResult(WordSetDto? WordSet);