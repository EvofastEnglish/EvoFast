using BuildingBlocks.Pagination;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;

namespace EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;

public record GetQuestionsByWordSetQuery(PaginationRequest PaginationRequest, Guid WordSetId) 
    : IQuery<GetQuestionsByWordSetResult>;
public record GetQuestionsByWordSetResult(PaginatedResult<QuestionDto> Questions);