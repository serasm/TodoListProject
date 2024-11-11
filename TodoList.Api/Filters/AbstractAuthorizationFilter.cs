using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoList.Api.Filters.Helpers;
using TodoList.Application.Services;

namespace TodoList.Api.Filters;

public abstract class AbstractAuthorizationFilter : Attribute, IAuthorizationFilter
{
    protected abstract string Role { get; }
    protected IAuthenticationService _authenticationService;

    public AbstractAuthorizationFilter() {}
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isUserAuthorized = IsUserAuthorized(context.HttpContext);

        if (!isUserAuthorized)
        {
            context.Result = new UnauthorizedResult();
            throw new AuthorizationException();
        }
    }

    private bool IsUserAuthorized(HttpContext httpContext)
    {
        var authHeader = httpContext.FetchBearer();

        if (authHeader == null)
        {
            throw new AuthorizationException("Authorization header is missing.");
        }
        
        _authenticationService = httpContext.RequestServices.GetService<IAuthenticationService>();

        var token = _authenticationService.ValidateSignature(authHeader);

        if (token is null)
            throw new AuthorizationException("Token is empty.");
            
        var claimsIdentity = token.BuildClaimsIdentity();
                
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal.HasClaim(claim => claim.Type == ClaimTypes.Role && Role.Equals(claim.Value, StringComparison.InvariantCultureIgnoreCase));
    }
}