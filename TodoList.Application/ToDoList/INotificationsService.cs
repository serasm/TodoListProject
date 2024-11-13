namespace TodoList.Application.ToDoList;

public interface INotificationsService
{
    Task Send(string username, string email, string title, DateTime? dueDate);
}