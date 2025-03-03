namespace EvoFast.Application.Users.Commands.UpdateFcmToken;

public record UpdateFcmTokenCommand(UpdateFcmTokenRequest UpdateFcmTokenRequest) : ICommand<UpdateFcmTokenResult>;

public record UpdateFcmTokenRequest(string FcmToken, Guid UserId);

public record UpdateFcmTokenResult(string FcmToken);