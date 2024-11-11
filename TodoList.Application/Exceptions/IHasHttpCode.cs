using System.Net;

namespace TodoList.Application.Exceptions;

public interface IHasHttpCode
{
    HttpStatusCode StatusCode { get; }
}