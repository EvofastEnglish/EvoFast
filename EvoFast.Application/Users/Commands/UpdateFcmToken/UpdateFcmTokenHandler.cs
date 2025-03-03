using EvoFast.Application.Data;

namespace EvoFast.Application.Users.Commands.UpdateFcmToken;

public class UpdateFcmTokenHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateFcmTokenCommand, UpdateFcmTokenResult>
{
    public async Task<UpdateFcmTokenResult> Handle(UpdateFcmTokenCommand command, CancellationToken cancellationToken)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == command.UpdateFcmTokenRequest.UserId);
        if (user != null)
        {
            user.FcmToken = command.UpdateFcmTokenRequest.FcmToken;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return new UpdateFcmTokenResult(user.FcmToken ?? string.Empty);
    }
}