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
        
        
        
        builder
            .Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<TodoContext>();
                dbContextOptionsBuilder.UseNpgsql(_connectionString, b => b.MigrationsAssembly(typeof(TodoContext).Assembly.FullName));
                
                return new TodoContext(dbContextOptionsBuilder.Options);
            })
            .AsSelf()
            .As<DbContext>()
            .As<IdentityDbContext<User, IdentityRole<int>, int>>()
            .InstancePerLifetimeScope();
        
         builder.Register<UserStore<User, IdentityRole<int>, TodoContext, int>>(c =>
                new UserStore<User, IdentityRole<int>, TodoContext, int>(c.Resolve<TodoContext>()))
            .AsSelf()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

         /*builder.RegisterType<UserStore<User, IdentityRole<int>, TodoContext, int>>()
             .AsSelf()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope();*/
        builder.RegisterType<UserManager<User>>().AsSelf().InstancePerLifetimeScope();
    }
}