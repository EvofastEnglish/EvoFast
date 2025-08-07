using EvoFast.Application.Data;
using EvoFast.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Messages.Commands.DeleteMessages;

public class DeleteMessagesHandler(IApplicationDbContext dbContext, IChatGptService chatGptService)
    : ICommandHandler<DeleteMessagesCommand, DeleteMessagesResult>
{
    public async Task<DeleteMessagesResult> Handle(DeleteMessagesCommand command, CancellationToken cancellationToken)
    {
        var messages = await dbContext.Messages
            .Where(m => m.ConversationId == command.ConversationId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync(cancellationToken);

        if (messages.Count <= 1)
        {
            return new DeleteMessagesResult(true);
        }
        
        var messagesToDelete = messages.Skip(2);
        dbContext.Messages.RemoveRange(messagesToDelete);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteMessagesResult(true);
    }
}