namespace Business.Interfaces;

public interface IResponseResult
{
    bool Success { get; }
    int StatusCode { get; }
    string ErrorMessage { get; }
}

public interface IResponseResult<T> : IResponseResult
{
    T? Data { get; }
}