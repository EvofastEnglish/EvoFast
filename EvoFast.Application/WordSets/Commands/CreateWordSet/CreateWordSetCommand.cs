using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Commands.CreateWordSet;

public record CreateWordSetCommand(CreateWordSetRequest WordSet) : ICommand<CreateWordSetResult>;

public record CreateWordSetRequest(string WordSetName, string Description);

public record CreateWordSetResult(Guid Id);

public class CreateWordSetCommandValidator : AbstractValidator<CreateWordSetCommand>
{
    public CreateWordSetCommandValidator()
    {
        RuleFor(x => x.WordSet.WordSetName).NotEmpty().WithMessage("WordSetName is required.");
    }
}