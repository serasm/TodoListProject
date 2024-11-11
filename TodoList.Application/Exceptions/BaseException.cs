using System.Net;
using Serilog.Events;

namespace TodoList.Application.Exceptions;

public abstract class BaseException : Exception,
    IHasMessage,
    IHasSeverityLevel,
    IHasHttpCode
{
    public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    public virtual LogEventLevel Severity { get; set; } = LogEventLevel.Information;
    public virtual string UserMessage { get; set; } = ErrorMessageConsts.Default;

    public BaseException() {}

    public BaseException(Exception ex) : base(ex.Message, ex) {}
}