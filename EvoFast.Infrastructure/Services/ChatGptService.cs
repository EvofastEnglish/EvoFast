using System.Net.Http.Headers;
using System.Text;
using EvoFast.Application.Dtos;
using EvoFast.Application.Services;
using EvoFast.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EvoFast.Infrastructure.Services;

public class ChatGptService : IChatGptService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAISettings _settings;
    
    public ChatGptService(HttpClient httpClient, IOptions<OpenAISettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }
    
    public async Task<(string role, string content)> GetChatGptResponseAsync(List<ChatGptMessageDto> messages)
    {
        var requestBody = new
        {
            model = _settings.Model,
            messages = messages,
        };

        var requestContent = new StringContent(
            JsonConvert.SerializeObject(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var request = new HttpRequestMessage(HttpMethod.Post, _settings.ApiUrl)
        {
            Content = requestContent
        };
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

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