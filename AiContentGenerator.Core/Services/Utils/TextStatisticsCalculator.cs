using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Services.Utils;

// Статический класс-утилита для вычисления метрик текста в реальном времени
public static class TextStatisticsCalculator
{
    public static TextStatistics Calculate(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new TextStatistics
            {
                TotalCharacters = 0,
                CharactersWithoutSpaces = 0,
                WordCount = 0
            };
        }

        var totalChars = text.Length;
        var charsNoSpaces = text.Count(c => !char.IsWhiteSpace(c));
        var words = text.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        return new TextStatistics
        {
            TotalCharacters = totalChars,
            CharactersWithoutSpaces = charsNoSpaces,
            WordCount = words
        };
    }
}
