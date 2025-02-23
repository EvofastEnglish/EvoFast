using EvoFast.Application.Data;
using EvoFast.Domain.Models;

namespace EvoFast.Application.Users.Commands.CreateUser;

public class CreateUserHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(
            command.User.Id, 
            command.User.Email,
            command.User.Username,
            command.User.FirstName,
            command.User.LastName
            );
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateUserResult(user.Id);
    }
}