using System.Text;
using AiContentGenerator.Core.Domain.Models;

namespace AiContentGenerator.Core.Services.AI;

// Паттерн Mock-объект (Тестовая заглушка):
// Позволяет автономно разрабатывать и тестировать UI без реальных запросов к API,
// без траты денег на балансе и без запущенного локального llama-server.
public sealed class MockAiService : IAiService
{
    public event Action<string>? StatusChanged;

    public async Task<OperationResult<string>> GenerateTextAsync(
        PromptPackage prompt,
        double temperature,
        AppSettings settings,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Эмулируем этапы работы с реальной нейросетью через событие StatusChanged
            StatusChanged?.Invoke("Тестовый режим: эмуляция подключения к модели...");
            await Task.Delay(400, cancellationToken);

            StatusChanged?.Invoke("Тестовый режим: генерация демонстрационного контента...");
            await Task.Delay(600, cancellationToken);

            // Формируем чистый текст для редактора (без консольных рамок)
            var sb = new StringBuilder();
            sb.AppendLine("Демонстрационный результат генерации (Mock-режим)");
            sb.AppendLine();
            sb.AppendLine("Это пример сгенерированного текста для проверки верстки окна редактора, подсчета статистики слов и экспорта в файл.");
            sb.AppendLine();
            sb.AppendLine("1. Параметры шаблона успешно переданы из графического интерфейса в ядро.");
            sb.AppendLine($"2. Использована температура креативности: {temperature:F1}.");
            sb.AppendLine("3. Сетевые запросы не выполнялись, баланс API сохранен.");
            sb.AppendLine();
            sb.AppendLine("Вы можете отредактировать этот текст вручную, скопировать в буфер обмена или экспортировать в форматы TXT и Markdown.");

            StatusChanged?.Invoke("Генерация успешно завершена (Mock).");
            return OperationResult<string>.Success(sb.ToString().TrimEnd());
        }
        catch (OperationCanceledException)
        {
            StatusChanged?.Invoke("Операция генерации была отменена.");
            return OperationResult<string>.Failure("Генерация отменена пользователем.");
        }
    }
}
