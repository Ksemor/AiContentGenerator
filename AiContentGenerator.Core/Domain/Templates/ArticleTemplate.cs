using System.Text;
using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Domain.Templates;

// Реализует генерацию статьи для блога с поддержкой SEO-ключей и разделов
public sealed class ArticleTemplate : ContentTemplate
{
    public override string DisplayName => "Статья в блог";
    public string Keywords { get; set; } = string.Empty;
    public int SectionsCount { get; set; } = 3;
    public bool IncludeConclusion { get; set; } = true;

    public override OperationResult<bool> Validate()
    {
        var baseCheck = base.Validate();
        if (!baseCheck.IsSuccess)
        {
            return baseCheck;
        }

        if (SectionsCount is < 1 or > 10)
        {
            return OperationResult<bool>.Failure("Количество разделов статьи должно быть от 1 до 10.");
        }

        return OperationResult<bool>.Success(true);
    }

    // Сборка промпта с форматированием структуры статьи
    public override OperationResult<PromptPackage> BuildPrompt()
    {
        var validation = Validate();
        if (!validation.IsSuccess)
        {
            return OperationResult<PromptPackage>.Failure(validation.ErrorMessage);
        }

        var systemPrompt =
            $"Ты — профессиональный копирайтер. " +
            $"Пиши качественные статьи на языке: {Language}. " +
            $"{GetToneInstruction(Tone)}";

        var sb = new StringBuilder();
        sb.AppendLine($"Напиши статью на тему: «{Topic}».");
        sb.AppendLine(GetLengthInstruction(Length));
        sb.AppendLine($"Структурируй материал примерно на {SectionsCount} логических разделов с подзаголовками.");

        if (!string.IsNullOrWhiteSpace(Keywords))
        {
            sb.AppendLine($"Обязательно включи ключевые слова: {Keywords.Trim()}.");
        }

        if (IncludeConclusion)
        {
            sb.AppendLine("В конце статьи добавь блок с краткими выводами («Заключение»).");
        }

        var package = new PromptPackage(systemPrompt, sb.ToString().TrimEnd());
        return OperationResult<PromptPackage>.Success(package);
    }
}
