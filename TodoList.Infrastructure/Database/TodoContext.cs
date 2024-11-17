using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Database;

public class TodoContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
    
    public TodoContext() : base() {}

    public TodoContext(DbContextOptions<TodoContext> options): base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
    }
}