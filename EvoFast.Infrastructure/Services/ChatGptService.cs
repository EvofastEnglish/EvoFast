using System.Net.Http.Headers;
using System.Text;
using EvoFast.Application.Dtos;
using EvoFast.Application.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EvoFast.Infrastructure.Services;

public class ChatGptService : IChatGptService
{
    private readonly HttpClient _httpClient;
    private readonly string _openAiApiKey;
    private readonly string _openAiModel;
    
    public ChatGptService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _openAiApiKey = configuration["OpenAiSettings:ApiKey"];
        _openAiModel = configuration["OpenAiSettings:Model"];
    }
    
    public async Task<(string role, string content)> GetChatGptResponseAsync(List<ChatGptMessageDto> messages)
    {
        var requestBody = new
        {
            model = _openAiModel,
            messages = messages,
        };

        var requestContent = new StringContent(
            JsonConvert.SerializeObject(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
        {
            Content = requestContent
        };
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAiApiKey);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error calling OpenAI API");
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
        string role = result?.choices[0]?.message?.role ?? "assistant";
        string content = result?.choices[0]?.message?.content ?? "";
        return (role, content);
    }
}