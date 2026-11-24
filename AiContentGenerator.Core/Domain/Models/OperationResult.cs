namespace AiContentGenerator.Core.Domain.Models;

// Паттерн Result (OperationResult)
public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T? Data { get; set; }

    public OperationResult() { }

    public OperationResult(bool isSuccess, string errorMessage = "", T? data = default)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
    }

    // Фабричные методы для удобного возврата успеха или ошибки
    public static OperationResult<T> Success(T data) => new(true, string.Empty, data);
    public static OperationResult<T> Failure(string errorMessage) => new(false, errorMessage, default);
}
