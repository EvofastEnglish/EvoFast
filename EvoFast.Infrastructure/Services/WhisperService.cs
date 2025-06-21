using EvoFast.Application.Services;
using EvoFast.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAI.Audio;

namespace EvoFast.Infrastructure.Services;

public class WhisperService : IWhisperService
{
    private readonly OpenAiSettings _settings;
    public WhisperService(IOptions<OpenAiSettings> options)
    {
        _settings = options.Value;
    }
    public async Task<string> TranscribeAsync(IFormFile audioFile, string language)
    {
        var openApiKey = _settings.ApiKey;
        if (string.IsNullOrEmpty(openApiKey))
            throw new InvalidOperationException("Missing OpenAI API Key.");
        await using var stream = audioFile.OpenReadStream();
        var audioOptions = new AudioTranscriptionOptions
        {
            ResponseFormat = AudioTranscriptionFormat.Srt,
            Language = language,
        };
        var audioClient = new AudioClient(_settings.WhisperModel, openApiKey);
        var response = await audioClient.TranscribeAudioAsync(stream, audioFile.FileName, audioOptions);
        return response.Value.Text;
    }
}