using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
    
    protected virtual JwtSecurityToken ValidateSignature(string header) => _authenticationService.ValidateSignature(header);

    private bool IsUserAuthorized(HttpContext httpContext)
    {
        return true;
    }
}