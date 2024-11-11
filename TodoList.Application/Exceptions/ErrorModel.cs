using System.Net;

namespace TodoList.Application.Exceptions;

public class ErrorModel
{
    public string Message { get; private set; }
    public Uri RequestUri { get; private set; }
    public readonly DateTimeOffset TimeStamp;
    public readonly Guid ErrorId;

    private ErrorModel()
    {
        TimeStamp = DateTimeOffset.UtcNow;
        ErrorId = Guid.NewGuid();
    }

    public static ErrorModel Create(string message, Uri requestUri)
    {
        var error = new ErrorModel();
        error.Message = message;
        error.RequestUri = requestUri;
        return error;
    }
}