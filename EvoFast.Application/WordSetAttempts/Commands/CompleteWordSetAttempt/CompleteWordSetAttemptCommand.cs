namespace EvoFast.Application.WordSetAttempts.Commands.CompleteWordSetAttempt;

public record CompleteWordSetAttemptCommand(CompleteWordSetAttemptRequest WordSetAttempt) : ICommand<CompleteWordSetAttemptResult>;

public record CompleteWordSetAttemptRequest(Guid Id);
public record CompleteWordSetAttemptResult(Guid Id);

public class CompleteWordSetAttemptCommandValidator : AbstractValidator<CompleteWordSetAttemptCommand>
{
    public CompleteWordSetAttemptCommandValidator()
    {
        RuleFor(x => x.WordSetAttempt.Id).NotEmpty().WithMessage("WordSetAttemptId is required.");
    }
}
