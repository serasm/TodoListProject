using TodoList.Core.Models;

namespace TodoList.Application.Services;

public interface IHashService
{
    bool CompareHash(User user, string plainText, string hashedValue );
    string HashPassword(User user, string plainText);
}