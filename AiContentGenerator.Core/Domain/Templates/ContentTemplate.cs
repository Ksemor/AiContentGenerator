using AiContentGenerator.Core.Domain.Enums;
using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Domain.Templates;

// Задает общий каркас для всех типов контента и инкапсулирует общие поля
public abstract class ContentTemplate
{
    private double _temperature = 0.7;

    public abstract string DisplayName { get; }
    public string Topic { get; set; } = string.Empty;
    public ToneOfVoice Tone { get; set; } = ToneOfVoice.Official;
    public ContentLength Length { get; set; } = ContentLength.Medium;
    public string Language { get; set; } = "Русский";
    
    public double Temperature
    {
        get => _temperature;
        set => _temperature = Math.Clamp(value, 0.0, 1.0);
    }

    public virtual OperationResult<bool> Validate()
    {
        if (string.IsNullOrWhiteSpace(Topic))
        {
            return OperationResult<bool>.Failure("Поле «Тема» обязательно для заполнения.");
        }

        if (string.IsNullOrWhiteSpace(Language))
        {
            return OperationResult<bool>.Failure("Поле «Язык» не может быть пустым.");
        }

        return OperationResult<bool>.Success(true);
    }

    public abstract OperationResult<PromptPackage> BuildPrompt();

    protected static string GetToneInstruction(ToneOfVoice tone) => tone switch
    {
        ToneOfVoice.Official => "Стиль: официально-деловой, строгий, без просторечий.",
        ToneOfVoice.Friendly => "Стиль: дружелюбный, открытый, теплый и разговорный.",
        ToneOfVoice.Expert => "Стиль: экспертный, аналитический, с точной терминологией.",
        ToneOfVoice.Sales => "Стиль: продающий, убедительный, с акцентом на выгоду читателя.",
        ToneOfVoice.Humorous => "Стиль: с уместным легким юмором, иронией и метафорами.",
        _ => "Стиль: нейтральный."
    };

    protected static string GetLengthInstruction(ContentLength length) => length switch
    {
        ContentLength.Short => "Объем: краткий текст (примерно 70-150 слов), емко и по сути.",
        ContentLength.Medium => "Объем: средний текст (примерно 250-450 слов), оптимальная глубина.",
        ContentLength.Long => "Объем: подробный материал (от 600 слов и более) с абзацами.",
        _ => "Объем: средний сбалансированный текст."
    };
}
