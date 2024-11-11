using MediatR;
using TodoList.Application.Configuration;

namespace TodoList.Application.ToDoList;

public class SendItemNotificationRequest : IRequest
{
    
}

public class SendItemNotificationHandler : IRequestHandler<>
{
    private readonly INotificationsService _notificationsService;
    
    public SendItemNotificationHandler(INotificationsService notificationsService)
    {
        _notificationsService = notificationsService ?? throw new ArgumentNullException(nameof(notificationsService));
    }
}