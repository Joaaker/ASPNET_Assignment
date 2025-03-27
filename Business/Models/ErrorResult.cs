namespace Business.Models; 

public class ErrorResult : ResponseResult
{
    public ErrorResult(int statusCode, string errorMessage)
    {
        Success = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}
