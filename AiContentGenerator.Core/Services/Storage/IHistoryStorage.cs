using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Services.Storage;

// Контракт локального хранилища истории генераций
public interface IHistoryStorage
{
    // Паттерн Издатель-Подписчик
    event Action<GenerationRecord>? RecordAdded;

    Task<OperationResult<List<GenerationRecord>>> LoadAsync();

    Task<OperationResult<bool>> SaveAsync(List<GenerationRecord> records);

    Task<OperationResult<bool>> AppendAsync(GenerationRecord record);

    // Делегат-предикат Func
    OperationResult<List<GenerationRecord>> Filter(
        List<GenerationRecord> records,
        Func<GenerationRecord, bool> predicate);
}
