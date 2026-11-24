using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Services.AI;

// Контракт сервиса взаимодействия с ИИ
public interface IAiService
{
    // Паттерн Издатель-Подписчик
    event Action<string>? StatusChanged;

    // Асинхронная генерация контента с поддержкой отмены через CancellationToken
    Task<OperationResult<string>> GenerateTextAsync(
        PromptPackage prompt,
        double temperature,
        AppSettings settings,
        CancellationToken cancellationToken = default);
}
