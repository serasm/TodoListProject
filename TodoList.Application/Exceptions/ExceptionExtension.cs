using Serilog;

namespace TodoList.Application.Exceptions;

public static class ExceptionExtension
{
    public static ErrorModel ToErrorModel(this Exception exception, string address) => ErrorModel.Create(exception.GetExceptionMessage(), new Uri(address));
    
    public static void LogException(this ILogger logger, Exception exception, ErrorModel errorModel)
    {
        if (exception is IHasSeverityLevel hasSeverityLevel)
        {
            logger.Write(hasSeverityLevel.Severity, exception.Message,
                $"{errorModel.ErrorId} Message: {errorModel.Message} RequestUri: {errorModel.RequestUri}");
        }
        else
        {
            logger.Error(exception.Message,
                $"{errorModel.ErrorId} Message: {errorModel.Message} RequestUri: {errorModel.RequestUri}");
        }
    }

    private static string GetExceptionMessage(this Exception exception)
    {
        var userMessage = ErrorMessageConsts.Default;

        if (exception.GetBaseException() is IHasMessage baseMessage)
        {
            userMessage = baseMessage.UserMessage;
        }
        else if(exception is IHasMessage message)
        {
            userMessage = message.UserMessage;
        }
        
        return userMessage;
    }
}