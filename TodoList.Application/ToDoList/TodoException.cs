using System.Net;
using TodoList.Application.Exceptions;

namespace TodoList.Application.ToDoList;

public class TodoException : Exception, IHasHttpCode
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

    public TodoException(string message) : base(message)
    {
        
    }
}