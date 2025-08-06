namespace EvoFast.Application.Messages.Commands.DeleteMessages;

public record DeleteMessagesCommand(Guid ConversationId) : ICommand<DeleteMessagesResult>;
public record DeleteMessagesResult(bool IsSuccess);

public class CreateMessageCommandValidator : AbstractValidator<DeleteMessagesCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty().WithMessage("ConversationId is required.");
    }
}