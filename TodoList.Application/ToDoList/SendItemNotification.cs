using MediatR;
using TodoList.Application.Configuration;

namespace TodoList.Application.ToDoList;

public class SendItemNotificationRequest : IRequest
{
    
}

public class SendItemNotificationHandler : IRequestHandler<SendItemNotificationRequest>
{
    private readonly INotificationsService _notificationsService;
    
    public SendItemNotificationHandler(INotificationsService notificationsService)
    {
        _notificationsService = notificationsService ?? throw new ArgumentNullException(nameof(notificationsService));
    }

    public Task Handle(SendItemNotificationRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}