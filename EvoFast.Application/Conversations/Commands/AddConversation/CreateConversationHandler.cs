using EvoFast.Application.Data;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.Conversations.Commands.AddConversation;

public class CreateConversationHandler(IApplicationDbContext dbContext)
: ICommandHandler<CreateConversationCommand, CreateConversationResult>
{
    public async Task<CreateConversationResult> Handle(CreateConversationCommand command, CancellationToken cancellationToken)
    {
        var conversation = command.Conversation.Adapt<Conversation>();
        conversation.CreatedAt = DateTime.UtcNow;
        dbContext.Conversations.Add(conversation);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateConversationResult(conversation.Id);    
    }
}