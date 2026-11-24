using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Services.Export;

// Семейство взаимозаменяемых алгоритмов экспорта
public interface IContentExporter
{
    string Extension { get; }

    string Description { get; }

    Task<OperationResult<bool>> ExportAsync(GenerationRecord record, string filePath);
}
