namespace ProductsApi.Common;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public List<string> ErrorMessage { get; set; } = new();

    public static ReturnResult<T> Success(T result) =>
        new() { IsSuccess = true, Result = result };

    public static ReturnResult<T> Fail(string error) =>
        new() { IsSuccess = false, ErrorMessage = new List<string> { error } };

    public static ReturnResult<T> Fail(List<string> errors) =>
        new() { IsSuccess = false, ErrorMessage = errors };
}
