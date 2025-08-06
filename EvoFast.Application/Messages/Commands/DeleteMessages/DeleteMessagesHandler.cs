using EvoFast.Application.Data;
using EvoFast.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.Messages.Commands.DeleteMessages;

public class DeleteMessagesHandler(IApplicationDbContext dbContext, IChatGptService chatGptService)
    : ICommandHandler<DeleteMessagesCommand, DeleteMessagesResult>
{
    public async Task<DeleteMessagesResult> Handle(DeleteMessagesCommand command, CancellationToken cancellationToken)
    {
        var messagesToDelete = await dbContext.Messages
            .Where(m => m.ConversationId == command.ConversationId)
            .ToListAsync(cancellationToken);

        dbContext.Messages.RemoveRange(messagesToDelete);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteMessagesResult(true);
    }
}