using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TodoList.Application.Services;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationConfig _config;
    private readonly SymmetricSecurityKey _key;

    public AuthenticationService(AuthenticationConfig config, SymmetricSecurityKey key)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public string AccessRole { get; } = "Access";
    public string GenerateSignedToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var plainToken = tokenHandler.CreateToken(CreateSecurityTokenDescriptor(new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature), CreateClaimsIdentity(user, AccessRole), _config.JwtAudience, _config.JwtLifetimeInMinutes));
        var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
        return signedAndEncodedToken;
    }

    public JwtSecurityToken ValidateSignature(string header)
    {
        throw new NotImplementedException();
    }

    private ClaimsIdentity CreateClaimsIdentity(User user, string role) => new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role)
        });

    private SecurityTokenDescriptor CreateSecurityTokenDescriptor(SigningCredentials credentials, ClaimsIdentity identity, string audience,
        int lifetimeInMinutes) =>  new SecurityTokenDescriptor()
        {
            Subject = identity,
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddMinutes(lifetimeInMinutes),
            Issuer = _config.JwtIssuer,
            Audience = audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(-lifetimeInMinutes),
        };
}