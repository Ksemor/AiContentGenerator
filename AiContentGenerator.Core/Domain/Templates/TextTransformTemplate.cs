using System.Text;
using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Domain.Templates;

// Шаблон рерайта и обработки готового текста (суммаризация, корректура, рерайт)
public sealed class TextTransformTemplate : ContentTemplate
{
    public override string DisplayName => "Рерайт / Обработка текста";
    public string SourceText { get; set; } = string.Empty;
    public string TransformationGoal { get; set; } = "Сделать глубокий рерайт с сохранением исходного смысла";

    public override OperationResult<bool> Validate()
    {
        if (string.IsNullOrWhiteSpace(Topic))
        {
            Topic = "Обработка текста";
        }

        var baseCheck = base.Validate();
        if (!baseCheck.IsSuccess)
        {
            return baseCheck;
        }

        if (string.IsNullOrWhiteSpace(SourceText))
        {
            return OperationResult<bool>.Failure("Поле «Исходный текст» обязательно для заполнения.");
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
            $"Ты — профессиональный редактор и корректор. " +
            $"Выполняй обработку текста на языке: {Language}. " +
            $"{GetToneInstruction(Tone)}";

        var sb = new StringBuilder();
        sb.AppendLine($"Задача обработки: {TransformationGoal}.");
        sb.AppendLine(GetLengthInstruction(Length));
        sb.AppendLine("Верни только результат преобразования без лишних вводных слов.");
        sb.AppendLine();
        sb.AppendLine("--- ИСХОДНЫЙ ТЕКСТ ---");
        sb.AppendLine(SourceText);
        sb.AppendLine("--- КОНЕЦ ИСХОДНОГО ТЕКСТА ---");

        var package = new PromptPackage(systemPrompt, sb.ToString().TrimEnd());
        return OperationResult<PromptPackage>.Success(package);
    }
}
