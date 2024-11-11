using TodoList.Core.Models;

namespace TodoList.Application.Repositories;

public interface IUserRepository
{
    User GetByUsername(string username);
}