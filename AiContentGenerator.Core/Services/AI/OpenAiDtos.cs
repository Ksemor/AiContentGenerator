using System.Text.Json.Serialization;

namespace AiContentGenerator.Core.Services.AI;

internal sealed class OpenAiChatRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
    
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("messages")] 
    public List<OpenAiMessageDto> Messages { get; set; } = new();
}

internal sealed class OpenAiMessageDto
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
    
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}

internal sealed class OpenAiChatResponse
{
    [JsonPropertyName("choices")]
    public List<OpenAiChoiceDto>? Choices { get; set; }
    
    [JsonPropertyName("error")]
    public OpenAiErrorDto? Error { get; set; }
}

internal sealed class OpenAiChoiceDto
{
    [JsonPropertyName("message")]
    public OpenAiMessageDto? Message { get; set; }
}
internal sealed class OpenAiErrorDto
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}