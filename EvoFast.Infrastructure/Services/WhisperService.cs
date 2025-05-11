using EvoFast.Application.Services;
using Microsoft.AspNetCore.Http;
using OpenAI.Audio;

namespace EvoFast.Infrastructure.Services;

public class WhisperService : IWhisperService
{
    public async Task<string> TranscribeAsync(IFormFile audioFile, string language)
    {
        var openApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (string.IsNullOrEmpty(openApiKey))
            throw new InvalidOperationException("Missing OpenAI API Key.");
        await using var stream = audioFile.OpenReadStream();
        var audioOptions = new AudioTranscriptionOptions
        {
            ResponseFormat = AudioTranscriptionFormat.Srt,
            Language = language,
        };
        var audioClient = new AudioClient("whisper-1", openApiKey);
        var response = await audioClient.TranscribeAudioAsync(stream, audioFile.FileName, audioOptions);
        return response.Value.Text;
    }
}