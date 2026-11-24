using AiContentGenerator.Core.Domain.Enums;
using AiContentGenerator.Core.Domain.Models;
using AiContentGenerator.Core.Domain.Templates;

namespace AiContentGenerator.Core.Services.Factories;

// Паттерн Фабричный метод
public static class TemplateFactory
{
    public static OperationResult<ContentTemplate> Create(TemplateType type) => type switch
    {
        TemplateType.Article => OperationResult<ContentTemplate>.Success(new ArticleTemplate()),
        TemplateType.SocialPost => OperationResult<ContentTemplate>.Success(new SocialPostTemplate()),
        TemplateType.Email => OperationResult<ContentTemplate>.Success(new EmailTemplate()),
        TemplateType.TextTransform => OperationResult<ContentTemplate>.Success(new TextTransformTemplate()),
        _ => OperationResult<ContentTemplate>.Failure($"Неподдерживаемый тип шаблона: {type}")
    };
}
