using TodoList.Core.Models;

namespace TodoList.Application.Repositories;

public interface IUserTodosRepository
{
    IQueryable<UserTodo> GetAllWaiting();
}