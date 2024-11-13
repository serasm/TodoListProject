using Autofac;
using TodoList.Application.Repositories;
using TodoList.Application.ToDoList;
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
    }
}