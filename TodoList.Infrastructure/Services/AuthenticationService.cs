using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoList.Application.Exceptions;
using TodoList.Application.Services;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationConfig _config;
    private readonly SymmetricSecurityKey _key;

    public AuthenticationService(AuthenticationConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSignatureSecret));
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
        try
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[] { null },
                ValidIssuers = new string[] { _config.JwtIssuer },
                IssuerSigningKey = _key
            };
            tokenValidationParameters.ValidateAudience = false;
        
            var tokenHandler = new JwtSecurityTokenHandler();
            
            tokenHandler.ValidateToken(header, tokenValidationParameters, out var validatedToken);
            
            return validatedToken as JwtSecurityToken;
        }
        catch (Exception ex)
        {
            throw new AuthorizationException(ex);
        }
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