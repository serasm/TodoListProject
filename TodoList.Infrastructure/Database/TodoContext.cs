using Microsoft.EntityFrameworkCore;

namespace TodoList.Infrastructure.Database;

public class TodoContext : DbContext
{
    public TodoContext(): base() {}

    public TodoContext(DbContextOptions<TodoContext> options): base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
}