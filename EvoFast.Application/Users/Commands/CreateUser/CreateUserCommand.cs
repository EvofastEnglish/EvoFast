namespace EvoFast.Application.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserRequest User) : ICommand<CreateUserResult>;

public record CreateUserRequest(Guid Id, string Email, string Username, string? FirstName, string? LastName);

public record CreateUserResult(Guid Id);