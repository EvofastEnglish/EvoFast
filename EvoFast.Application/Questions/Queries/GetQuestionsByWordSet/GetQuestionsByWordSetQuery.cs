using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;

namespace EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;

public record GetQuestionsByWordSetQuery(Guid WordSetId) 
    : IQuery<GetQuestionsByWordSetResult>;
public record GetQuestionsByWordSetResult(IEnumerable<QuestionDto> Questions);