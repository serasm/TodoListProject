using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Repositories;
using TodoList.Application.ToDoList;
using TodoList.Core.Models;
using TodoList.Infrastructure.Database.Repositories;

namespace TodoList.Infrastructure.Database;

public class DatabaseModule : Autofac.Module
{
    private readonly string _connectionString;
    
    public DatabaseModule(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UsersRepository>().As<IUsersRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<TodosRepository>().As<ITodosRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<UserTodosRepository>().As<IUserTodosRepository>()
            .InstancePerLifetimeScope();
        
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<TodoContext>();
        dbContextOptionsBuilder.UseNpgsql(_connectionString);
                
        var ctx = new TodoContext(dbContextOptionsBuilder.Options);
        
        builder
            .Register(c => ctx)
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
        builder.Register<UserStore<User, IdentityRole<int>, TodoContext, int>>(c => new UserStore<User, IdentityRole<int>, TodoContext, int>(ctx)).AsImplementedInterfaces();
        builder.RegisterType<UserManager<User>>();
    }
}