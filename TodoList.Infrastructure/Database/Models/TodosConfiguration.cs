using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Database.Models;

public class TodosConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Todos");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.IsDone).IsRequired();
        
        builder.HasOne<User>(x => x.Owner)
            .WithMany(owner => owner.Todos)
            .HasForeignKey(x => x.OwnerId);
    }
}