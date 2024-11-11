using System.IdentityModel.Tokens.Jwt;
using TodoList.Core.Models;

namespace TodoList.Application.Services;

public interface IAuthenticationService
{
    string AccessRole { get; }
    string GenerateSignedToken(User user);
    JwtSecurityToken ValidateSignature(string header);
}