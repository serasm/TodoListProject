using Microsoft.AspNetCore.Identity;
using TodoList.Application.Repositories;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Database.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly UserManager<User> _userManager;

    public UsersRepository(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }
    
    public async Task Create(User user)
    {
        await _userManager.CreateAsync(user);
    }

    public Task<User?> GetByUsername(string username)
    {
        return _userManager.FindByNameAsync(username);
    }

    public Task<User?> GetById(int userId)
    {
        return _userManager.FindByIdAsync(userId.ToString());
    }
}