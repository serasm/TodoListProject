using Serilog.Events;

namespace TodoList.Application.Exceptions;

public interface IHasSeverityLevel
{
    LogEventLevel Severity { get; }
}