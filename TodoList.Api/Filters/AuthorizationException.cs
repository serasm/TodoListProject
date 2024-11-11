using System.Net;
using Serilog.Events;
using TodoList.Application.Exceptions;

namespace TodoList.Api.Filters;

public class AuthorizationException : BaseException
{
    public override string UserMessage { get; set; } = "Your login token is expired, invalid or missing.";
    public override LogEventLevel Severity { get; set; } = LogEventLevel.Warning;
    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unauthorized;

    public AuthorizationException() {}

    public AuthorizationException(string userMessage)
    {
        userMessage = userMessage;
    }
}