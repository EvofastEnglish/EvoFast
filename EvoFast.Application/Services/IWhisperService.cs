using Microsoft.AspNetCore.Http;

namespace EvoFast.Application.Services;

public interface IWhisperService
{
    Task<string> TranscribeAsync(IFormFile audioFile, string language);
}