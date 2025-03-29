using EvoFast.Application.Dtos;

namespace EvoFast.Application.Messages.Commands.CreateMessage;

public record CreateMessageCommand(CreateMessageRequest MessageRequest) : ICommand<CreateMessageResult>;
public record CreateMessageRequest(Guid ConversationId, String Content);

public record CreateMessageResult(MessageDto Message);

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x => x.MessageRequest.ConversationId).NotEmpty().WithMessage("ConversationId is required.");
        RuleFor(x => x.MessageRequest.Content).NotEmpty().WithMessage("Content is required.");
    }
}