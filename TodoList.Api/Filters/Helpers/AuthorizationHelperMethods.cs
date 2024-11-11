using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TodoList.Api.Filters.Helpers;

public static class AuthorizationHelperMethods
{
    public const string BearerSchemaName = "Bearer";

    public static string FetchBearer(this HttpContext context)
    {
        string requestToken = string.Empty;

        var authRequest = context.Request.Headers.Authorization.ToString();

        if (authRequest.Contains(BearerSchemaName, StringComparison.InvariantCultureIgnoreCase))
        {
            requestToken = authRequest.Replace(BearerSchemaName, string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();
        }
        
        return requestToken;
    }
    
    public static ClaimsIdentity BuildClaimsIdentity(this JwtSecurityToken jwtSecurityToken) => new ClaimsIdentity(jwtSecurityToken?.Claims, BearerSchemaName, "unique_name", "Access");
}