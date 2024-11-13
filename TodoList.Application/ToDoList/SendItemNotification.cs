using MediatR;
using TodoList.Application.Repositories;

namespace TodoList.Application.ToDoList;

public class SendItemNotificationRequest : IRequest
{
    
}

public class SendItemNotificationHandler : IRequestHandler<SendItemNotificationRequest>
{
    private readonly INotificationsService _notificationsService;
    private readonly IUserTodosRepository _userTodosRepository;
    
    public SendItemNotificationHandler(INotificationsService notificationsService, IUserTodosRepository userTodosRepository)
    {
        _notificationsService = notificationsService ?? throw new ArgumentNullException(nameof(notificationsService));
        _userTodosRepository = userTodosRepository ?? throw new ArgumentNullException(nameof(userTodosRepository));
    }

    public async Task Handle(SendItemNotificationRequest request, CancellationToken cancellationToken)
    {
        var todos = _userTodosRepository.GetAllWaiting();

        foreach (var todo in todos)
        {
            await _notificationsService.Send(todo.Username, todo.Email, todo.Title, todo.DueDate);
        }
    }
}