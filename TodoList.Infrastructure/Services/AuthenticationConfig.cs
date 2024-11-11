namespace TodoList.Infrastructure.Services;

public class AuthenticationConfig
{
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public int JwtLifetimeInMinutes { get; set; }
    public string JwtSignatureSecret { get; set; }
    public string BasicAuthScheme { get; set; }
}