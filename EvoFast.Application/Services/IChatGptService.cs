using EvoFast.Application.Dtos;

namespace EvoFast.Application.Services;

public interface IChatGptService
{
    Task<(string role, string content)> GetChatGptResponseAsync(List<ChatGptMessageDto> messages);

}