
namespace EvoFast.Application.WordSetAttempts.Commands.CreateWordSetAttempt;

public record CreateWordSetAttemptCommand(CreateWordSetAttemptRequest WordSetAttempt) : ICommand<CreateWordSetAttemptResult>;

public record CreateWordSetAttemptRequest(Guid WordSetId, Guid UserId);

public record CreateWordSetAttemptResult(Guid Id);

public class CreateWordSetAttemptCommandValidator : AbstractValidator<CreateWordSetAttemptCommand>
{
    public CreateWordSetAttemptCommandValidator()
    {
        RuleFor(x => x.WordSetAttempt.WordSetId).NotEmpty().WithMessage("WordSetId is required.");
        RuleFor(x => x.WordSetAttempt.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}