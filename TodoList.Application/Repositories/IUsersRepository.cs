using TodoList.Core.Models;

namespace TodoList.Application.Repositories;

public interface IUsersRepository
{
    Task Create(User user);
    Task<User?> GetByUsername(string username);
    Task<User?> GetById(int userId);
}