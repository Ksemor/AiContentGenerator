namespace AiContentGenerator.Core.Domain.Models;

public class AppSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public bool UseMockService {get; set;} = false;
}