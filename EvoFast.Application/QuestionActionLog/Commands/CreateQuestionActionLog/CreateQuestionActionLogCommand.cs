namespace EvoFast.Application.QuestionActionLog.Commands.CreateQuestionActionLog;

public record CreateQuestionActionLogCommand(CreateQuestionActionLogRequest Request, Guid UserId) : ICommand<CreateQuestionActionLogResult>;
public record CreateQuestionActionLogRequest(Guid QuestionId, bool IsCorrect);
public record CreateQuestionActionLogResult(Guid Id);