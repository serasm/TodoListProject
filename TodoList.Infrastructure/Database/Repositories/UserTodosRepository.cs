using TodoList.Application.Repositories;
using TodoList.Application.ToDoList;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Database.Repositories;

public class UserTodosRepository : IUserTodosRepository
{
    private readonly TodoContext _context;
    
    public UserTodosRepository(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public IQueryable<UserTodo> Get()
    {
        return _context.UserTodos.AsQueryable();
    }
}