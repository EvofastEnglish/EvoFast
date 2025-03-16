namespace EvoFast.Application.Questions.Commands.DeleteAnswer;

public record DeleteAnswerCommand(Guid QuestionId, Guid AnswerId) : ICommand<DeleteAnswerResult>;
public record DeleteAnswerResult(bool IsSuccess);