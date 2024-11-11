using Microsoft.AspNetCore.Identity;
using TodoList.Application.Services;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Services;

public class HashService : IHashService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public HashService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public bool CompareHash(User user, string plainText, string hashedValue)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentNullException(nameof(plainText));

        if (string.IsNullOrWhiteSpace(hashedValue))
            throw new ArgumentNullException(nameof(hashedValue));
        
        var result = _passwordHasher.VerifyHashedPassword(user, hashedValue, plainText);
        
        return result != PasswordVerificationResult.Failed;
    }

    public string HashPassword(User user, string plainText)
    {
        if(user is null)
            throw new ArgumentNullException(nameof(user));
        if(string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentNullException(nameof(plainText));
        
        return _passwordHasher.HashPassword(user, plainText);
    }
}