namespace AiContentGenerator.Core.Domain.Models;

public sealed class PromptPackage
{
    public string SystemPrompt { get; }
    public string UserPrompt { get; }

    public PromptPackage(string systemPrompt, string userPrompt)
    {
        SystemPrompt = systemPrompt ?? string.Empty;
        UserPrompt = userPrompt ??  string.Empty;
    }
}