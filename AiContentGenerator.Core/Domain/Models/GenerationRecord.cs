namespace AiContentGenerator.Core.Domain.Models;

public sealed class GenerationRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CteatedAt { get; set; } = DateTime.UtcNow;
    public string TemplateName { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Tone { get; set; } = string.Empty;
    public double Temperature {get; set;}
    public string ResultText { get; set; } = string.Empty;
}