using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Questions.Queries.GetQuestionsByWordSet;

public class GetQuestionByWordSetHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetQuestionsByWordSetQuery, GetQuestionsByWordSetResult>
{
    public async Task<GetQuestionsByWordSetResult> Handle(GetQuestionsByWordSetQuery request, CancellationToken cancellationToken)
    {
        var questions = dbContext.Questions
            .Include(q => q.Answers)
            .Where(q => q.WordSetId == request.WordSetId);
        var questionDtos = questions.Adapt<List<QuestionDto>>();
        return new GetQuestionsByWordSetResult(questionDtos);
    }
}