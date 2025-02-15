namespace EvoFast.Application.WordSets.Commands.UpdateWordSet;

public record UpdateWordSetCommand(UpdateWordSetRequest WordSet) : ICommand<UpdateWordSetResult>;

public record UpdateWordSetRequest(Guid Id, string WordSetName, string Description);
public record UpdateWordSetResult(Guid Id);

public class UpdateWordSetCommandValidator : AbstractValidator<UpdateWordSetCommand>
{
    public UpdateWordSetCommandValidator()
    {
        RuleFor(x => x.WordSet.WordSetName).NotEmpty().WithMessage("WordSetName is required.");
    }
}
