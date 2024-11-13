using TodoList.Core.Models;

namespace TodoList.Application.ToDoList;

public interface ITodosRepository
{
    Task Create(Todo item);

    Task<IQueryable<Todo>> GetForUser(int userId);
}