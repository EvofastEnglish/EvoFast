
namespace EvoFast.Application.WordSets.Commands.CreateWordSet;

public record CreateWordSetCommand(CreateWordSetRequest WordSet) : ICommand<CreateWordSetResult>;

public record CreateWordSetRequest(long NumberId);

public record CreateWordSetResult(Guid Id);

public class CreateWordSetCommandValidator : AbstractValidator<CreateWordSetCommand>
{
    public CreateWordSetCommandValidator()
    {
        RuleFor(x => x.WordSet.NumberId).NotEmpty().WithMessage("NumberId is required.");
    }
}