namespace TodoList.Application.Exceptions;

public interface IHasMessage
{
    string UserMessage { get; }
}