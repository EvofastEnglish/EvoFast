using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Application.QuestionActionLog.Commands.CreateQuestionActionLog;

public class CreateQuestionActionLogHandler(IApplicationDbContext dbContext)
: ICommandHandler<CreateQuestionActionLogCommand, CreateQuestionActionLogResult>
{
    public async Task<CreateQuestionActionLogResult> Handle(CreateQuestionActionLogCommand request, CancellationToken cancellationToken)
    {
        var questionActionLogs = new QuestionActionLogs
        {
            QuestionId = request.Request.QuestionId,
            UserId = request.UserId,
            IsCorrect = request.Request.IsCorrect,
            CreatedAt = DateTime.Now
        };
        dbContext.QuestionActionLogs.Add(questionActionLogs);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateQuestionActionLogResult(questionActionLogs.Id);
    }
}