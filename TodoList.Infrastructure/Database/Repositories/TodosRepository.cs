using TodoList.Application.ToDoList;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Database.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly TodoContext _context;
    
    public TodosRepository(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task Create(Todo item)
    {
        await _context.Todos.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public Task<IQueryable<Todo>> GetForUser(int userId)
    {
        return Task.FromResult(_context.Todos.Where(x => x.OwnerId == userId));
    }
}