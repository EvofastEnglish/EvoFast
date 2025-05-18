using Microsoft.Extensions.AI;

namespace EvoFast.Application.Mapper;

public static class ChatRoleMapper
{
    public static ChatRole MapFromDbRole(string? dbRole)
    {
        return dbRole?.ToLower() switch
        {
            "user" => ChatRole.User,
            "assistant" => ChatRole.Assistant,
            "system" => ChatRole.System,
            _ => ChatRole.User
        };
    }
}