using Newtonsoft.Json;

namespace EvoFast.Application.Dtos;

public record ChatGptMessageDto(
    [property: JsonProperty("role")] string Role,
    [property: JsonProperty("content")] string Content);
