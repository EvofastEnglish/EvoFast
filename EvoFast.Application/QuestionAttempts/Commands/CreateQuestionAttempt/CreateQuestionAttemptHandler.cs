using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttempt;

public class CreateQuestionAttemptHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateQuestionAttemptCommand, CreateQuestionAttemptResult>
{
    public async Task<CreateQuestionAttemptResult> Handle(CreateQuestionAttemptCommand command, CancellationToken cancellationToken)
    {
        var questionAttempt = dbContext.QuestionAttempts
            .Include(qt => qt.Question)
            .ThenInclude(q => q.Answers)
            .FirstOrDefault(qt => qt.QuestionId == command.QuestionAttempt.QuestionId 
                                  && qt.WordSetAttemptId == command.QuestionAttempt.WordSetAttemptId);
        if (questionAttempt == null)
        {
            questionAttempt = new QuestionAttempt
            {
                QuestionId = command.QuestionAttempt.QuestionId,
                WordSetAttemptId = command.QuestionAttempt.WordSetAttemptId,
                IsBookmarked = command.QuestionAttempt.IsBookmarked
            };
            dbContext.QuestionAttempts.Add(questionAttempt);
        }
        else
        {
            questionAttempt.IsBookmarked = command.QuestionAttempt.IsBookmarked;
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateQuestionAttemptResult(questionAttempt.Adapt<QuestionAttemptDto>());
    }
}