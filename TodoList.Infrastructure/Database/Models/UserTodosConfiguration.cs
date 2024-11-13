using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Database.Models;

public class UserTodosConfiguration : IEntityTypeConfiguration<UserTodo>
{
    public void Configure(EntityTypeBuilder<UserTodo> builder)
    {
        builder.HasNoKey();
        builder.ToView("UserTodos");
    }
}