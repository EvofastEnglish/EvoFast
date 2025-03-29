namespace EvoFast.Application.Conversations.Commands.AddConversation;

public record CreateConversationCommand(CreateConversationRequest Conversation) : ICommand<CreateConversationResult>;

public record CreateConversationRequest(
    String YourRole,
    String AIRole,
    String Topic,
    Guid UserId);
    
public record CreateConversationResult(Guid Id);

public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.Conversation.YourRole).NotEmpty().WithMessage("YourRole is required.");
        RuleFor(x => x.Conversation.AIRole).NotEmpty().WithMessage("AIRole is required.");
        RuleFor(x => x.Conversation.Topic).NotEmpty().WithMessage("Topic is required.");
        RuleFor(x => x.Conversation.UserId).NotEmpty().WithMessage("UserId is required.");

    }
}
