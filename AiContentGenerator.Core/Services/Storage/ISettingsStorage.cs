using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Services.Storage;

// Контракт для сохранения и загрузки конфигурации подключения к API
public interface ISettingsStorage
{
    Task<OperationResult<AppSettings>> LoadAsync();

    Task<OperationResult<bool>> SaveAsync(AppSettings settings);
}
