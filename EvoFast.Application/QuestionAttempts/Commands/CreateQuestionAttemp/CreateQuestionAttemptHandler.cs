using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttemp;

public class CreateQuestionAttemptHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateQuestionAttemptCommand, CreateQuestionAttemptResult>
{
    public async Task<CreateQuestionAttemptResult> Handle(CreateQuestionAttemptCommand command, CancellationToken cancellationToken)
    {
        var questionAttempt = dbContext.QuestionAttempts
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
        return new CreateQuestionAttemptResult(questionAttempt.Id);
    }
}