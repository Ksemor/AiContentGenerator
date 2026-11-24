using System.Text;
using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Domain.Templates;

// Формирует промпт для постов в соцсети с поддержкой хештегов и эмодзи
public sealed class SocialPostTemplate : ContentTemplate
{
    public override string DisplayName => "Пост для соцсетей";
    public string Platform { get; set; } = "Telegram";
    public bool IncludeHashtags { get; set; } = true;
    public bool IncludeEmoji { get; set; } = true;

    public override OperationResult<bool> Validate()
    {
        var baseCheck = base.Validate();
        if (!baseCheck.IsSuccess)
        {
            return baseCheck;
        }

        if (string.IsNullOrWhiteSpace(Platform))
        {
            return OperationResult<bool>.Failure("Укажите социальную сеть (например, Telegram или VK).");
        }

        return OperationResult<bool>.Success(true);
    }

    public override OperationResult<PromptPackage> BuildPrompt()
    {
        var validation = Validate();
        if (!validation.IsSuccess)
        {
            return OperationResult<PromptPackage>.Failure(validation.ErrorMessage);
        }

        var systemPrompt =
            $"Ты — профессиональный SMM-специалист. " +
            $"Пиши вовлекающие посты для площадки {Platform} на языке: {Language}. " +
            $"{GetToneInstruction(Tone)}";

        var sb = new StringBuilder();
        sb.AppendLine($"Напиши публикацию для {Platform} на тему: «{Topic}».");
        sb.AppendLine(GetLengthInstruction(Length));
        sb.AppendLine("Сделай цепляющий заголовок в первой строке.");

        if (IncludeEmoji)
        {
            sb.AppendLine("Используй подходящие эмодзи для разделения пунктов и акцентов.");
        }
        else
        {
            sb.AppendLine("Не используй эмодзи, только чистый текст.");
        }

        if (IncludeHashtags)
        {
            sb.AppendLine("В самом конце добавь 3-5 тематических хештегов.");
        }

        var package = new PromptPackage(systemPrompt, sb.ToString().TrimEnd());
        return OperationResult<PromptPackage>.Success(package);
    }
}
