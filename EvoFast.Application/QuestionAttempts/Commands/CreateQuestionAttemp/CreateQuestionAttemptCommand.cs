namespace EvoFast.Application.QuestionAttempts.Commands.CreateQuestionAttemp;

public record CreateQuestionAttemptCommand(CreateQuestionAttemptRequest QuestionAttempt) : ICommand<CreateQuestionAttemptResult>;

public record CreateQuestionAttemptRequest(Guid QuestionId, Guid WordSetAttemptId, bool IsBookmarked);

public record CreateQuestionAttemptResult(Guid Id);

public class CreateQuestionAttemptCommandValidator : AbstractValidator<CreateQuestionAttemptCommand>
{
    public CreateQuestionAttemptCommandValidator()
    {
        RuleFor(x => x.QuestionAttempt.QuestionId).NotEmpty().WithMessage("QuestionId is required.");
        RuleFor(x => x.QuestionAttempt.WordSetAttemptId).NotEmpty().WithMessage("WordSetAttemptId is required.");
    }
}