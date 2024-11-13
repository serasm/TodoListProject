using Serilog;
using TodoList.Application.ToDoList;

namespace TodoList.Infrastructure.Services;

public class NotificationService : INotificationsService
{
    private readonly ILogger _logger;
    
    public NotificationService(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task Send(string username, string email, string title, DateTime? dueDate)
    {
        var message = $"Dear {username}, your todo {title} is not done";
        if (dueDate.HasValue)
            message = $"Dear {username}, your todo {title} will end at {dueDate} and is not done";
        
        _logger.Information($"Message sent to {email}: {message}");
        
        return Task.CompletedTask;
    }
}