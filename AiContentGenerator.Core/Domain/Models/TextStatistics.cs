namespace AiContentGenerator.Core.Domain.Models;

public sealed record TextStatistics
{
    public int TotalCharacters { get; init; }
    public int CharactersWithoutSpaces { get; init; }
    public int WordCount { get; init; }
}