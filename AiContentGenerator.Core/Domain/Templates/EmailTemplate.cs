using System.Text;
using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Domain.Templates;

// Формирует структуру официального или делового письма с адресатом и CTA
public sealed class EmailTemplate : ContentTemplate
{
    public override string DisplayName => "Деловое письмо";
    public string Recipient { get; set; } = "Партнер";
    public string Purpose { get; set; } = string.Empty;
    public string CallToAction { get; set; } = string.Empty;
    
    public override OperationResult<bool> Validate()
    {
        var baseCheck = base.Validate();
        if (!baseCheck.IsSuccess)
        {
            return baseCheck;
        }

        if (string.IsNullOrWhiteSpace(Recipient))
        {
            return OperationResult<bool>.Failure("Укажите получателя письма (например, Руководитель, Клиент, Коллега).");
        }

        if (string.IsNullOrWhiteSpace(Purpose))
        {
            return OperationResult<bool>.Failure("Укажите основную цель письма.");
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
            $"Ты — эксперт по деловой переписке и коммуникациям. " +
            $"Составляй выверенные деловые электронные письма на языке: {Language}. " +
            $"{GetToneInstruction(Tone)}";

        var sb = new StringBuilder();
        sb.AppendLine($"Составь электронное письмо на тему: «{Topic}».");
        sb.AppendLine($"Получатель: {Recipient}.");
        sb.AppendLine($"Цель письма: {Purpose}.");
        sb.AppendLine(GetLengthInstruction(Length));

        if (!string.IsNullOrWhiteSpace(CallToAction))
        {
            sb.AppendLine($"Включи четкий призыв к действию: {CallToAction.Trim()}.");
        }

        sb.AppendLine("Письмо должно содержать строку темы, вежливое приветствие, основную часть и подпись.");

        var package = new PromptPackage(systemPrompt, sb.ToString().TrimEnd());
        return OperationResult<PromptPackage>.Success(package);
    }
}
